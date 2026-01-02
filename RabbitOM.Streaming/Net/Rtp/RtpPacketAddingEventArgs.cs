using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpPacketAddingEventArgs : EventArgs
    {
        public RtpPacketAddingEventArgs( RtpPacket packet  )
        {
            Packet = packet ?? throw new ArgumentNullException( nameof( packet ) );
        }

        public RtpPacket Packet { get; }

        public bool Continue { get; set; }
    }
}
