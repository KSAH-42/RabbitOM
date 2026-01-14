using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    /// <summary>
    /// Represent a packet inspector. This component is used to detected errors, or something that potentially introduce issues before dispatching the packet to others components.
    /// </summary>
    public abstract class PacketInspector
    {
        public abstract void Inspect( RtpPacket packet );
    }
}
