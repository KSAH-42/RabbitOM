using System;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public abstract class RtcpPacket
    {
        public const byte DefaultVersion = 2;


        protected RtcpPacket( byte version ) { Version = version; }

        public byte Version { get; private set; }
    }
}
