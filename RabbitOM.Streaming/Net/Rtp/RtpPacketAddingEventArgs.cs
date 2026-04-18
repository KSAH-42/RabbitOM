using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpPacketAddingEventArgs : EventArgs
    {
        private readonly RtpPacket _packet;
        private bool _canContinue;

        public RtpPacketAddingEventArgs( RtpPacket packet  )
        {
            _packet = packet ?? throw new ArgumentNullException( nameof( packet ) );
            _canContinue = true;
        }

        public RtpPacket Packet
        {
            get => _packet;
        }

        public bool CanContinue
        {
            get => _canContinue;
            set => _canContinue = value;
        }
    }
}
