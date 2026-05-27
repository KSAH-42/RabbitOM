using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspInterleaveMessage : RtspMessage
    {
        public int Channel { get; set; }

        public ushort Length { get; set; }

        public byte[] Buffer { get; set; }
    }
}
