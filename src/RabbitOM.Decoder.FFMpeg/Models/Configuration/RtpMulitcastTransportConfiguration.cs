namespace RabbitOM.Decoder.FFMpeg.Models.Configuration
{
	public sealed class RtpMulitcastTransportConfiguration
	{
		public string? Address { get; set; }
		public int? Port { get; set; }
		public byte? TTL { get; set; }
		public TimeSpan? ReceiveTimeout { get; set; }
		public TimeSpan? RetryInterval { get; set; }
	}
}
