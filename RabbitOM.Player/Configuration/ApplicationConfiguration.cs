using System;

namespace RabbitOM.Player.Configuration
{
	public sealed class ApplicationConfiguration
	{
		public ServerConfiguration Server { get; } = new ServerConfiguration();

		public List<VideoSourceConfiguration> VideoSources { get; } = new List<VideoSourceConfiguration>();
	}
}
