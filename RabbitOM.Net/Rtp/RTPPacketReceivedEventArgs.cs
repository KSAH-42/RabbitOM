using System;

namespace RabbitOM.Net.Rtp
{
    public class RTPPacketReceivedEventArgs : EventArgs
    {
        private readonly RTPPacket _packet;

        public RTPPacketReceivedEventArgs( RTPPacket packet )
        {
            _packet = packet ?? throw new ArgumentNullException( nameof( packet ) );
        }

        public RTPPacket Packet 
        {
            get => _packet;
        }
    }
}