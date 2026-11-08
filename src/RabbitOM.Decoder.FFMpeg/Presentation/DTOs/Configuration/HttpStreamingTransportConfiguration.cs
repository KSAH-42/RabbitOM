namespace RabbitOM.Decoder.FFMpeg.Presentation.Dtos.Configuration
{
	public sealed class HttpStreamingTransportConfiguration
	{
		public string? EndPoint { get; set; }

		public bool? EnableCompression { get; set; }

		public TimeSpan? RetryInterval { get; set; }
	}
}
