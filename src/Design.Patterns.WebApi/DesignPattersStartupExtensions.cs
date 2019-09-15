using Design.Patterns.Core;
using Design.Patterns.WebApi.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi
{
	public static class DesignPattersStartupExtensions
	{
		private const string dbConnectionString = "Data Source=./sqlite-database/DesignPatterns.sqlite";

		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			return services
				.AddTransient<IDbConnection>(_ => new SQLiteConnection(dbConnectionString))
				.AddTransient<IRepository<UserState>, UserRepository>();
		}

		public static IServiceCollection AddHandlers(this IServiceCollection services)
		{
			return services
				.AddTransient<IUserCommandHandlers, UserCommandHandlers>()
				.AddTransient<IUserQueryHandlers, UserQueryHandlers>();
		}
	}
}
