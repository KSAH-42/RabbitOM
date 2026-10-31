namespace RabbitOM.Decoder.FFMpeg.Models
{
	public sealed class RtspChannelStatus
	{
		public bool IsStarted { get; set; }
		public bool IsConnected { get; set; }
		public string[]? Errors { get; set; }
		public RtspChannelMetrics Metrics { get; } = new RtspChannelMetrics();
	}
}
