namespace RabbitOM.Decoder.FFMpeg.Data.DTOs.Configuration
{
	public sealed class UdpTransportConfiguration
	{
		public int? Port { get; set; }

		public TimeSpan? ReceiveTimeout { get; set; }

		public TimeSpan? RetryInterval { get; set; }
	}
}
