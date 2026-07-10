using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public sealed class RtspPacket
    {
        public byte Channel { get; }

        public byte[] Payload { get; }
    }
}