using System;

namespace RabbitOM.Player.Configuration
{
	using Microsoft.Extensions.Configuration;

	public sealed class ConfigurationProvider
	{
		public static ConfigurationProvider Default { get; } = new ConfigurationProvider();

		public IConfiguration Configuration { get; } = new ConfigurationBuilder()
				.SetBasePath( AppContext.BaseDirectory )
				.AddJsonFile( "appsettings.json" , false , true )
				.Build();
	}
}
