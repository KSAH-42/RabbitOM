namespace RabbitOM.Decoder.FFMpeg.Models.Configuration
{
	public sealed class HttpSenderTransportConfiguration : TransportConfiguration
	{
		public string? EndPoint { get; set; }
		public TimeSpan RetryInterval { get; set; }
	}
}
