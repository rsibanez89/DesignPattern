using Design.Patterns.WebApi.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Text;

namespace Design.Patterns.WebApi
{
	public static class DesignPattersStartupConfiguration
	{
		public static IServiceCollection ConfigureDesignPattersDependencyInjection(this IServiceCollection services, IConfiguration config)
		{
			// Set up logging
			Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.WriteTo.Console(new CompactJsonFormatter())
				.CreateLogger();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				var sharedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysupers3cr3tsharedkey!"));
				options.TokenValidationParameters = new TokenValidationParameters
				{
					// Clock skew compensates for server time drift.
					// We recommend 5 minutes or less:
					ClockSkew = TimeSpan.FromMinutes(5),
					// Specify the key used to sign the token:
					IssuerSigningKey = sharedKey,
					RequireSignedTokens = true,
					// Ensure the token hasn't expired:
					RequireExpirationTime = true,
					ValidateLifetime = true,
					// Ensure the token audience matches our audience value (default true):
					ValidateAudience = true,
					ValidAudience = "api://default",
					// Ensure the token was issued by a trusted authorization server (default true):
					ValidateIssuer = true,
					ValidIssuer = "https://ribanez.com.ar"
				};
			});

			services.AddAuthorization(options =>
			{
			});

			return services.AddUsersModule(config);
		}
	}
}
