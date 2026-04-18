using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public class RtpPacketAddingEventArgs : EventArgs
    {
        private readonly RtpPacket _packet;
        private bool _canContinue;

        public RtpPacketAddingEventArgs( RtpPacket packet  )
            : this ( packet , true )
        {
        }

        public RtpPacketAddingEventArgs( RtpPacket packet , bool canContinue )
        {
            _packet = packet ?? throw new ArgumentNullException( nameof( packet ) );
            _canContinue = canContinue;
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
