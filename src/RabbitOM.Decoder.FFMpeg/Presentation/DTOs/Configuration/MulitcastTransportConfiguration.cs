namespace RabbitOM.Decoder.FFMpeg.Presentation.DTOs.Configuration
{
	public sealed class MulitcastTransportConfiguration
	{
		public string? Address { get; set; }

		public int? Port { get; set; }

		public byte? TTL { get; set; }

		public TimeSpan? ReceiveTimeout { get; set; }

		public TimeSpan? RetryInterval { get; set; }
	}
}
