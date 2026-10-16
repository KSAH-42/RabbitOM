using System;

namespace RabbitOM.Player.Configuration
{
	using Microsoft.Extensions.Configuration;

	public sealed class ApplicationConfigurationSource
	{
		private readonly IConfiguration _configuration;

		public ApplicationConfigurationSource( IConfiguration configuration )
		{
			_configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );
		}

		public ApplicationConfiguration GetConfiguration()
		{
			return _configuration.GetSection( "application" ).Get<ApplicationConfiguration>() ?? new ApplicationConfiguration();
		}
	}
}
