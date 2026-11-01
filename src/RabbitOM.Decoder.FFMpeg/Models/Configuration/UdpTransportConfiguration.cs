namespace RabbitOM.Decoder.FFMpeg.Models.Configuration
{
	public sealed class UdpTransportConfiguration
	{
		public int? Port { get; set; }

		public TimeSpan? ReceiveTimeout { get; set; }

		public TimeSpan? RetryInterval { get; set; }
	}
}
