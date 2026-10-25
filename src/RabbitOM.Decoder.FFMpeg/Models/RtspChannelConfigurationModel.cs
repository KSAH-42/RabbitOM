namespace RabbitOM.Decoder.FFMpeg.Models
{
	public sealed class RtspChannelConfigurationModel
	{
		public string? Uri { get; set; }
		public TimeSpan? ReceiveTimeout { get; set; }
		public TimeSpan? SendTimeout { get; set; }
		public TimeSpan? RetryTimeout { get; set; }
		public TimeSpan? RetryInterval { get; set; }
		public TransportConfigurationModel? OutputTransport { get; set; }
	}
}
