using Dapper;
using Design.Patterns.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Design.Patterns.WebApi.CommonHelpers
{
	/// <summary>
	/// Implements the IRepository by using SQLite
	/// </summary>
	public class SQLiteRepository<TState>
		: IRepository<TState>
		where TState : State
	{
		public IDbConnection DBConnection { get; private set; }
		public string TableName { get; private set; }
		public IEnumerable<string> TableColumns { get; private set; }

		public SQLiteRepository(IDbConnection connection)
		{
			DBConnection = connection;

			var name = typeof(TState).Name;
			if (name.EndsWith("State"))
			{
				TableName = name[0..^5];
			}

			TableColumns = typeof(TState).GetProperties().Select(x => x.Name);
		}

		public Task<IEnumerable<TState>> ListAsync()
		{
			return DBConnection.QueryAsync<TState>($"SELECT * FROM {TableName} WHERE DeletedOn IS NULL");
		}

		public Task<TState> GetAsync(long entityId)
		{
			return DBConnection.QuerySingleAsync<TState>(
				$"SELECT * FROM {TableName} WHERE ID = @EntityID AND DeletedOn IS NULL",
				new
				{
					EntityID = entityId
				}, commandType: CommandType.Text);
		}

		public async Task<TState> CreateAsync(TState state)
		{
			var now = DateTime.Now;
			state.CreatedOn = now;
			state.CreatedBy = -1; // Need a way to get the userId that is doing this request
			state.LastModifiedOn = now;
			state.LastModifiedBy = -1; // Need a way to get the userId that is doing this request
			state.Version = 1;

			// Id value will be generated in Database
			var columsWithoutId = TableColumns.Where(name => name != "Id");

			string columns = string.Join(", ", columsWithoutId);
			string columnsAt = string.Join(", ", columsWithoutId.Select(x => $"@{x}"));

			state.Id = await DBConnection.ExecuteAsync(
				@$"INSERT INTO {TableName} ({columns}) VALUES ({columnsAt});
				SELECT last_insert_rowid()", state);

			return state;
		}

		public async Task<TState> UpdateAsync(TState state)
		{
			var now = DateTime.Now;
			return await UpdateOnDateAsync(state, now);
		}


		public async Task<TState> DeleteAsync(TState state)
		{
			var now = DateTime.Now;
			state.DeletedOn = now;
			state.DeletedBy = -1; // Need a way to get the userId that is doing this request
			return await UpdateOnDateAsync(state, now);

		}

		private async Task<TState> UpdateOnDateAsync(TState state, DateTime time)
		{
			state.LastModifiedOn = time;
			state.LastModifiedBy = -1; // Need a way to get the userId that is doing this request
			state.Version++;

			string updates = string.Join(", ", TableColumns.Select(x => $"{x} = @{x}"));

			var changes = await DBConnection.ExecuteAsync(
				@$"UPDATE {TableName} SET {updates} WHERE ID = @Id AND Version = {state.Version - 1};
				SELECT changes()", state);

			if (changes > 0)
			{
				return state;
			}

			throw new ApplicationException($"You are trying to update an old version of the entity {TableName}. Refresh the page and try again.");

		}
	}
}
