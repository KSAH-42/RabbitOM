using System;

namespace RabbitOM.Net.RtspV2
{
    public sealed class RtspPacket
    {
        public byte Channel { get; }

        public byte[] Payload { get; }
    }
}