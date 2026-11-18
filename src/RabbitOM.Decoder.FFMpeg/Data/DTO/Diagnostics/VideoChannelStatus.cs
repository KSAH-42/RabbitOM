namespace RabbitOM.Decoder.FFMpeg.Data.DTOs.Diagnostics
{
	public sealed class VideoChannelStatus
	{
		public bool IsStarted { get; set; }

		public bool IsConnected { get; set; }

		public VideoChannelMetrics Metrics { get; } = new VideoChannelMetrics();
	}
}
