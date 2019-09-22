using Design.Patterns.WebApi.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Compact;

namespace Design.Patterns.WebApi
{
	public static class DesignPattersStartupConfiguration
	{
		public static IServiceCollection ConfigureDesignPattersProject(this IServiceCollection services, IConfiguration config)
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
