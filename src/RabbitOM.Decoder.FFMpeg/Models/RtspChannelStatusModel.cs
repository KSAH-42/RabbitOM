namespace RabbitOM.Decoder.FFMpeg.Models
{
	public sealed class RtspChannelStatusModel
	{
		public bool IsStarted { get; set; }
		public bool IsConnected { get; set; }
		public string[]? Errors { get; set; }
		public RtspChannelMetricsModel Metrics { get; } = new RtspChannelMetricsModel();
	}
}
