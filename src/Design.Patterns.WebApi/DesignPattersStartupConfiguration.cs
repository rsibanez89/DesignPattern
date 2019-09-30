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

			return services.AddUsersModule(config);
		}
	}
}
