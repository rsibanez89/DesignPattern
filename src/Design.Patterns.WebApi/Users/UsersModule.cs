using Dapper;
using Design.Patterns.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Data.SQLite;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

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
			var userModuleOptions = config.GetSection("UsersModule");
			services.Configure<UserModuleOptions>(userModuleOptions);

			// Add Repositories
			services
				.AddTransient<IDbConnection>(_ => new SQLiteConnection(config.GetConnectionString("DesignPatternsDatabase")))
				.AddTransient<IUserRepository, UserRepository>();

			// Add Dapper Mapper for UserRole
			SqlMapper.AddTypeHandler(new UserRoleTypeHandler());

			// Add Services
			services
				.AddTransient<IPasswordService, PasswordService>()
				.AddTransient<IJwtBearerService, JwtBearerService>();

			// Add Handlers
			services
				.AddTransient<IUserCommandHandlers, UserCommandHandlers>()
				.AddTransient<IUserQueryHandlers, UserQueryHandlers>();

			//JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			services.AddAuthentication(options =>
			{
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					// Clock skew compensates for server time drift.
					// We recommend 5 minutes or less:
					ClockSkew = TimeSpan.FromMinutes(5),
					// Specify the key used to sign the token:
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(userModuleOptions.GetValue<string>("jwtIssuerSigningKey"))),
					RequireSignedTokens = true,
					// Ensure the token hasn't expired:
					RequireExpirationTime = false,
					ValidateLifetime = true,
					// Ensure the token audience matches our audience value (default true):
					ValidateAudience = true,
					ValidAudience = userModuleOptions.GetValue<string>("jwtAudience"),
					// Ensure the token was issued by a trusted authorization server (default true):
					ValidateIssuer = true,
					ValidIssuer = userModuleOptions.GetValue<string>("jwtIssuer"),
					
				};
			});

			services.AddAuthorization(options => 
			{
				options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
					.RequireAuthenticatedUser()
					.Build();

				options.AddPolicy("IsAdmin", policy => policy.RequireAuthenticatedUser().RequireRole("Admin").Build());
			});


			return services;
		}
	}
}
