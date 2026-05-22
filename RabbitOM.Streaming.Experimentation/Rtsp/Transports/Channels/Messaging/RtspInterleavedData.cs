using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging
{
    public sealed class RtspInterleavedData : IStreamElement
    {
        public int Channel { get; set; }

        public byte[] Buffer { get; set; }
    }
}
