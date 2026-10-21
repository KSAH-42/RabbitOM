namespace RabbitOM.Decoder.FFMpeg.Models
{
	public sealed class ChannelMetrics
	{
		public string? Codec { get; set; }
        public string? Protocol { get; set; }
        public string? Transport { get; set; }
        public bool ConnectionStatus { get; set; }
        public long Clock { get; set; }
        public long BytesReceivedPerSecond { get; set; }
        public long PacketReceivedPerSecond { get; set; }
        public long FrameCountPerSecond { get; set; }
        public long FrameWidth { get; set; }
        public long FrameHeight { get; set; }
        public long PacketsLostCount { get; set; }
        public long MaxFrameCountPerSecond { get; set; }
        public long MaxBytesReceivedPerSecond { get; set; }
        public long MaxPacketReceivedPerSecond { get; set; }
	}
}
