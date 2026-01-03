using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpPacketReceivedEventArgs : EventArgs
    {
        public RtpPacketReceivedEventArgs( RtpPacket packet  )
        {
            Packet = packet ?? throw new ArgumentNullException( nameof( packet ) );
        }

        public RtpPacket Packet { get; }
    }
}
