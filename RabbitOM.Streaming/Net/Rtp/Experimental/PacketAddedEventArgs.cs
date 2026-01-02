using System;

namespace RabbitOM.Streaming.Net.Rtp.Experimental
{
    public class PacketAddedEventArgs : EventArgs
    {
        public PacketAddedEventArgs( RtpPacket packet  )
        {
            Packet = packet;
        }

        public RtpPacket Packet { get; }
    }
}
