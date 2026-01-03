using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    /// <summary>
    /// Represent a packet inspector. This component is used to detected errors, or something that potentially introduce issues.
    /// </summary>
    public abstract class RtpPacketInspector
    {
        public abstract void Inspect( RtpPacket packet );
    }
}
