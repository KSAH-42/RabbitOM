namespace RabbitOM.Decoder.FFMpeg.Models.Configuration
{
	public sealed class RtpUnicastTransportConfiguration
	{
		public int? Port { get; set; }
		public TimeSpan? ReceiveTimeout { get; set; }
		public TimeSpan? RetryInterval { get; set; }
	}
}
