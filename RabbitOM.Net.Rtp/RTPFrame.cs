/*
 EXPERIMENTATION of the next implementation of the rtp layer

                    IMPLEMENTATION  NOT COMPLETED


*/

using System;

namespace RabbitOM.Net.Rtp
{
    public sealed class RTPFrame
    {
        private readonly RTPPacket[] _packets;



        public RTPFrame( RTPPacket[] packets ) 
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



        public RTPPacket[] Packets 
        {
            get => _packets;
        }



        public bool TryValidate()
        {
            return _packets != null && _packets.Length > 0;
        }
    }
}