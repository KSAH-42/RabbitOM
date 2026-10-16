using System;

namespace RabbitOM.Net.RtspV2.Transports
{
    public sealed class RtspInterleavedMessage : RtspMessage
    {
        public byte Channel { get; set; }

        public ushort Length { get; set; }

        public byte[] Buffer { get; set; }
    }
}
