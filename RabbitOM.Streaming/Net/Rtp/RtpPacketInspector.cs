using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public abstract class RtpPacketInspector
    {
        public abstract void Inspect( RtpPacket packet );
    }
}
