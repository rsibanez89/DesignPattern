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
	public class UserRepository : SQLiteRepository<UserState>
	{
		public UserRepository(IDbConnection connection) : base(connection)
		{
		}
	}
}
