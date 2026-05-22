using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Models
{
    public sealed class InterleavedStreamElement : IStreamElement
    {
        public int ChannelNumber { get; set; }
        public int Length { get; set; }
        public byte[] Buffer { get; set; }
    }
}
