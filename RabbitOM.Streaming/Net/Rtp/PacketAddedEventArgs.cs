using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class PacketAddedEventArgs : EventArgs
    {
        public PacketAddedEventArgs( RtpPacket packet  )
        {
            Packet = packet ?? throw new ArgumentNullException( nameof( packet ) );
        }

        public RtpPacket Packet { get; }
    }
}
