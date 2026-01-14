using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class PacketAddingEventArgs : EventArgs
    {
        public PacketAddingEventArgs( RtpPacket packet  )
        {
            Packet = packet ?? throw new ArgumentNullException( nameof( packet ) );
        }

        public RtpPacket Packet { get; }

        public bool CanContinue { get; set; }
    }
}
