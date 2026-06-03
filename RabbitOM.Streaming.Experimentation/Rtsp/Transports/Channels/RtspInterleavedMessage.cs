using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspInterleavedMessage : RtspMessage
    {
        public byte Channel { get; set; }

        public ushort Length { get; set; }

        public byte[] Buffer { get; set; }
    }
}
