using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpFilteringPacketEventArgs : EventArgs
    {
        public RtpFilteringPacketEventArgs( RtpPacket packet  )
        {
            Packet = packet ?? throw new ArgumentNullException( nameof( packet ) );
        }

        public RtpPacket Packet { get; }

        public bool CanContinue { get; set; }
    }
}
