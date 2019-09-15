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

		public SQLiteRepository(IDbConnection connection)
		{
			DBConnection = connection;

			var name = typeof(TState).Name;
			if (name.EndsWith("State"))
			{
				TableName = name[0..^5];
			}
		}

		public Task<IEnumerable<TState>> ListAsync()
		{
			return DBConnection.QueryAsync<TState>($"SELECT * FROM {TableName}");
		}

		public Task<TState> GetAsync(long entityId)
		{
			return DBConnection.QuerySingleAsync<TState>(
				$"SELECT * FROM {TableName} WHERE ID = @EntityID",
				new
				{
					EntityID = entityId
				});
		}

		public Task<TState> CreateAsync(long entityId)
		{
			
			return null;
		}

		public Task<TState> GetOrCreateAsync(long entityId)
		{
			throw new NotImplementedException();
		}

		public async Task<TState> SaveChangesAsync(TState state)
		{
			string columns = string.Join(", ", state.GetType().GetProperties().Select(x => x.Name));
			string columnsAt = string.Join(", ", state.GetType().GetProperties().Select(x => $"@{x.Name}"));

			string values = string.Join(", ", state.GetType().GetProperties().Select(x => x.GetValue(state)));

			await DBConnection.ExecuteAsync($"INSERT INTO {TableName} ({columns}) VALUES ({columnsAt})",
				state);

			return state;
		}
	}
}
