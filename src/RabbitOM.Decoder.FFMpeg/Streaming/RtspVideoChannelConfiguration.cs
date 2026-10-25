namespace RabbitOM.Decoder.FFMpeg.Streaming
{
	public sealed class RtspVideoChannelConfiguration
	{
		public required string? Uri { get; init; }
		public required TimeSpan? ReceiveTimeout { get; init; }
		public required TimeSpan? SendTimeout { get; init; }
		public required TimeSpan? RetryTimeout { get; init; }
		public required TimeSpan? RetryInterval { get; init; }
	}
}
