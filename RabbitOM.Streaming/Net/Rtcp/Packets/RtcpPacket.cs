using System;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public abstract class RtcpPacket
    {
        public const uint DefaultVersion = 2;

        public uint Version { get; protected set; }
        
        public bool Bit { get; protected set; }
    }
}
