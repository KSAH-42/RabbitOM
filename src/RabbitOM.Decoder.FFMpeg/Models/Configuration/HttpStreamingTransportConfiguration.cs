namespace RabbitOM.Decoder.FFMpeg.Models.Configuration
{
	public sealed class HttpStreamingTransportConfiguration
	{
		public string? EndPoint { get; set; }

		public bool? EnableCompression { get; set; }

		public TimeSpan? RetryInterval { get; set; }
	}
}
