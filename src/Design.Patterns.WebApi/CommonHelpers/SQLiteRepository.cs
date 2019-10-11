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
	public class SQLiteRepository<TEntity, TMessageContext>
		: IRepository<TEntity, TMessageContext>
		where TEntity : Entity
		where TMessageContext : MessageContext
	{
		public IDbConnection DBConnection { get; private set; }
		public string TableName { get; private set; }
		public IEnumerable<string> TableColumns { get; private set; }

		public SQLiteRepository(IDbConnection connection)
		{
			DBConnection = connection;

			var name = typeof(TEntity).Name;
			if (name.EndsWith("Entity"))
			{
				TableName = name[0..^6];
			}

			TableColumns = typeof(TEntity).GetProperties().Select(x => x.Name);
		}

		public Task<IEnumerable<TEntity>> ListAsync(TMessageContext messageContext)
		{
			return DBConnection.QueryAsync<TEntity>($"SELECT * FROM {TableName} WHERE DeletedOn IS NULL");
		}

		public Task<TEntity> GetAsync(long entityId, TMessageContext messageContext)
		{
			return DBConnection.QuerySingleAsync<TEntity>(
				$"SELECT * FROM {TableName} WHERE ID = @EntityID AND DeletedOn IS NULL",
				new
				{
					EntityID = entityId
				}, commandType: CommandType.Text);
		}

		public async Task<TEntity> CreateAsync(TEntity state, TMessageContext messageContext)
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

		public async Task<TEntity> UpdateAsync(TEntity entity, TMessageContext messageContext)
		{
			var now = DateTime.Now;
			return await UpdateOnDateAsync(entity, now);
		}


		public async Task<TEntity> DeleteAsync(TEntity entity, TMessageContext messageContext)
		{
			var now = DateTime.Now;
			entity.DeletedOn = now;
			entity.DeletedBy = -1; // Need a way to get the userId that is doing this request
			return await UpdateOnDateAsync(entity, now);

		}

		private async Task<TEntity> UpdateOnDateAsync(TEntity entity, DateTime time)
		{
			entity.LastModifiedOn = time;
			entity.LastModifiedBy = -1; // Need a way to get the userId that is doing this request
			entity.Version++;

			string updates = string.Join(", ", TableColumns.Select(x => $"{x} = @{x}"));

			var changes = await DBConnection.ExecuteAsync(
				@$"UPDATE {TableName} SET {updates} WHERE ID = @Id AND Version = {entity.Version - 1};
				SELECT changes()", entity);

			if (changes > 0)
			{
				return entity;
			}

			throw new ApplicationException($"You are trying to update an old version of the entity {TableName}. Refresh the page and try again.");

		}
	}
}
