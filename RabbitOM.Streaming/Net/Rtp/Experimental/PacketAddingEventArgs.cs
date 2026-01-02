using System;

namespace RabbitOM.Streaming.Net.Rtp.Experimental
{
    public class PacketAddingEventArgs : EventArgs
    {
        public PacketAddingEventArgs( RtpPacket packet  )
        {
            Packet = packet;
        }

        public RtpPacket Packet { get; }

        public bool Continue { get; set; } = true;
    }
}
