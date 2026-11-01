namespace RabbitOM.Decoder.FFMpeg.Application.Models
{
	public sealed class ChannelStatus
	{
		public bool IsStarted { get; set; }

		public bool IsConnected { get; set; }

		public string[]? Errors { get; set; }

		public ChannelMetrics Metrics { get; } = new ChannelMetrics();
	}
}
