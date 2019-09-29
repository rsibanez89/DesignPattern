using Design.Patterns.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SQLite;

namespace Design.Patterns.WebApi.Users
{
	public class UserModuleOptions
	{
		public string PasswordPepper { get; set; }
		public string JwtIssuerSigningKey { get; set; }
		public string JwtIssuer { get; set; }
		public string JwtAudience { get; set; }
	}

	public static class UsersModule
	{
		public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration config)
		{
			// Add User Module Options
			services.Configure<UserModuleOptions>(config.GetSection("UsersModule"));

			// Add Repositories
			services
				.AddTransient<IDbConnection>(_ => new SQLiteConnection(config.GetConnectionString("DesignPatternsDatabase")))
				.AddTransient<IUserRepository, UserRepository>();

			// Add Services
			services
				.AddTransient<IPasswordService, PasswordService>()
				.AddTransient<IJwtBearerService, JwtBearerService>();

			// Add Handlers
			return services
				.AddTransient<IUserCommandHandlers, UserCommandHandlers>()
				.AddTransient<IUserQueryHandlers, UserQueryHandlers>();
		}
	}
}
