using System;

namespace RabbitOM.Streaming.Rtp.Framing
{
    public sealed class RtpFrame
    {
        private readonly RtpPacket[] _packets;



        public RtpFrame( RtpPacket[] packets ) 
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