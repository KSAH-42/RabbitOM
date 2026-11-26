namespace RabbitOM.Decoder.FFMpeg.Streaming.Channels
{
	public sealed class RtspVideoChannelConfiguration
	{
		public static RtspVideoChannelConfiguration Default { get; } = new RtspVideoChannelConfiguration();

		public string? Uri { get; init; }
		public TimeSpan? ReceiveTimeout { get; init; }
		public TimeSpan? SendTimeout { get; init; }
		public TimeSpan? RetryTimeout { get; init; }
		public TimeSpan? RetryInterval { get; init; }
		public string? Transport { get; init; }
		public TimeSpan? TransportReceiveTimeout { get; init; }
		public int? Rtp { get; init; }
	}
}
