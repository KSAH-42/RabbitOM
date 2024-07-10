using System;

namespace RabbitOM.Streaming.Rtp.Framing
{
    public sealed class RTPFrame
    {
        private readonly RtpPacket[] _packets;



        public RTPFrame( RtpPacket[] packets ) 
        {
            if ( packets == null )
            {
                throw new ArgumentNullException( nameof( packets ) );
            }

            if ( packets.Length == 0 )
            {
                throw new ArgumentException( nameof( packets ) );
            }

            _packets = packets;
        }



        public RtpPacket[] Packets 
        {
            get => _packets;
        }
    }
}