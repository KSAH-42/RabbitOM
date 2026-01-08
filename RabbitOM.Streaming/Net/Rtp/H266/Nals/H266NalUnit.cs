using System;

namespace RabbitOM.Streaming.Net.Rtp.H266.Nals
{
    public struct H266NalUnit
    {
        public bool ForbiddenBit { get; private set; }

        public bool ZBit { get; private set; }

        public byte LayerId { get; private set; }

        public byte Type { get; private set; }

        public byte TemporalId { get; private set; }

        public ArraySegment<byte> Payload { get; private set; }





        public static bool TryParse( in ArraySegment<byte> buffer , out H266NalUnit result )
        {
            result = default;

            if ( buffer.Array == null || buffer.Count < 2 )
            {
                return false;
            }

            var header = ( buffer.Array[ buffer.Offset ] << 8 ) | ( buffer.Array[ buffer.Offset + 1 ] );

            result = new H266NalUnit();

            result.ForbiddenBit = ( ( header >> 15 ) & 0x1 ) == 1;
            result.ZBit         = ( ( header >> 14 ) & 0x1 ) == 1;
            result.LayerId      = (byte) ( ( header >> 8 ) & 0x3F );
            result.Type         = (byte) ( ( header >> 3 ) & 0x1F );
            result.TemporalId   = (byte) ( header & 0x07 );

            if ( buffer.Count > 2 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 2 , buffer.Count - 2 );
            }

            return true;
        }
    }           
}
