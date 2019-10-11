using Dapper;
using Design.Patterns.Core;
using Design.Patterns.WebApi.CommonHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public interface IUserRepository : IRepository<UserEntity, MessageContext>
	{
		Task<UserEntity> GetUserByEmail(string email);
	}

	public class UserRepository
		: SQLiteRepository<UserEntity, MessageContext>,
		IUserRepository
	{
		public UserRepository(IDbConnection connection) : base(connection)
		{
		}

		public Task<UserEntity> GetUserByEmail(string email)
		{
			return DBConnection.QuerySingleOrDefaultAsync<UserEntity>($"SELECT * FROM {TableName} WHERE Email = @Email AND DeletedOn IS NULL", new { Email = email });
		}
	}
}
