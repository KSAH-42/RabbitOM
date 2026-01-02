using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpPacketAddedEventArgs : EventArgs
    {
        public RtpPacketAddedEventArgs( RtpPacket packet  )
        {
            Packet = packet ?? throw new ArgumentNullException( nameof( packet ) );
        }

        public RtpPacket Packet { get; }
    }
}
