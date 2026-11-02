namespace RabbitOM.Decoder.FFMpeg.Presentation.Dtos
{
	public sealed class VideoChannelStatus
	{
		public bool IsStarted { get; set; }

		public bool IsConnected { get; set; }

		public VideoChannelMetrics Metrics { get; } = new VideoChannelMetrics();
	}
}
