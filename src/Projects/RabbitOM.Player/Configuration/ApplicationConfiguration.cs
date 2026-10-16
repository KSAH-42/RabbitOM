using System;

namespace RabbitOM.Player.Configuration
{
	public sealed class ApplicationConfiguration
	{
		public ServerConfiguration Server { get; } = new ServerConfiguration();

		public List<VideoSourceConfiguration> VideoSources { get; } = new List<VideoSourceConfiguration>();

		public ApplicationConfiguration ToUnProtect( IDataProtection dataProtection )
		{
			if ( dataProtection == null )
			{
				throw new ArgumentNullException( nameof( dataProtection ) );
			}

			var configuration = new ApplicationConfiguration();

			configuration.Server.ApiKey = dataProtection.UnProtect( Server.ApiKey );
			configuration.VideoSources.AddRange( VideoSources.Select( element => new VideoSourceConfiguration()
			{
				Uri = dataProtection.UnProtect( element.Uri )
			}));

			return configuration;
		}
	}
}
