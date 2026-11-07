// TODO: for validation don't use use string.IsNullOrWhiteSpace string don't trim or  throw exception in case
namespace RabbitOM.Decoder.FFMpeg.Presentation.DTOs
{
	public sealed class VideoChannelConfiguration
	{
		public string? Uri { get; set; }

		public TimeSpan? ReceiveTimeout { get; set; }

		public TimeSpan? SendTimeout { get; set; }

		public TimeSpan? RetryTimeout { get; set; }

		public TimeSpan? RetryInterval { get; set; }

		public string? TranportType { get; set; }

		public UdpTransportConfiguration? UdpTransport { get; set; }

		public MulitcastTransportConfiguration? MutlicastTransport { get; set; }

		public string? OutputTransportType { get; set; }

		public HttpStreamingTransportConfiguration? HttpOutputTransport { get; set; }

		public SharedMemoryTransportConfiguration? SharedMemoryTransport { get; set; }
	}
}
