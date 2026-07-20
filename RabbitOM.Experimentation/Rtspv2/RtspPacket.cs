using System;

namespace RabbitOM.Streaming.RtspV2
{
    public sealed class RtspPacket
    {
        public byte Channel { get; }

        public byte[] Payload { get; }
    }
}