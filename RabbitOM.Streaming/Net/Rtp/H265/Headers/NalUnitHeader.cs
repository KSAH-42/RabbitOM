using System;

namespace RabbitOM.Streaming.Net.Rtp.H265.Headers
{
    public struct NalUnitHeader
    {
        public static readonly NalUnitHeader Empty = new NalUnitHeader();





        public bool ForbiddenBit { get; private set; }
        public byte Type { get; private set; }
        public byte LayerId { get; private set; }
        public byte Tid { get; private set; }
        public ArraySegment<byte> Payload { get; private set; }
        


        

        
        // https://datatracker.ietf.org/doc/html/rfc7798#section-1.1.4

        public static bool TryParse( ArraySegment<byte> buffer , out NalUnitHeader result )
        {
            result = default;

            if ( buffer.Count <= 2 )
            {
                return false;
            }

            //  +---------------+---------------+
            //  |7|6|5|4|3|2|1|0|7|6|5|4|3|2|1|0|
            //  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            //  |F|   Type    |  LayerId  | TID |
            //  +-------------+-----------------+

            //  [payload........................]
            
            var header = ( buffer.Array[ buffer.Offset ] << 8 ) | ( buffer.Array[ buffer.Offset + 1 ] );
            
            result = new NalUnitHeader();

            result.ForbiddenBit  = (byte) ( ( header >> 15) & 0x01 ) == 1;
            result.Type          = (byte) ( ( header >> 9 ) & 0x3F );
            result.LayerId       = (byte) ( ( header >> 3 ) & 0x3F );
            result.Tid           = (byte) (   header        & 0x07 );

            if ( buffer.Count >= 3 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 2 , buffer.Array.Length - ( buffer.Offset + 2 ) );
            }

            return true;
        }
    } 
}