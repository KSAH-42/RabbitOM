using System;

namespace RabbitOM.Net.Rtp
{
    public abstract class RtpPacketInspector
    {
        public abstract void Inspect( RtpPacket packet );

        public abstract bool TryInspect( RtpPacket packet );
    }
}
