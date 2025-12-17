using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    public struct H265NalUnit
    {
        public bool ForbiddenBit { get; private set; }
        public H265NalUnitType Type { get; private set; }
        public byte LayerId { get; private set; }
        public byte Tid { get; private set; }
        public ArraySegment<byte> Payload { get; private set; }
        


        


        

        public static bool IsInvalidOrUnDefined( ref H265NalUnit nalUnit )
        {
            return nalUnit.Type == H265NalUnitType.INVALID || nalUnit.Type == H265NalUnitType.UNDEFINED;
        }

        // https://datatracker.ietf.org/doc/html/rfc7798#section-1.1.4
        
        //  +---------------+---------------+
        //  |7|6|5|4|3|2|1|0|7|6|5|4|3|2|1|0|
        //  +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        //  |F|   Type    |  LayerId  | TID |
        //  +-------------+-----------------+

        //  [payload........................]
            
        public static bool TryParse( ArraySegment<byte> buffer , out H265NalUnit result )
        {
            result = default;

            if ( buffer.Count < 2 )
            {
                return false;
            }

            var header = ( buffer.Array[ buffer.Offset ] << 8 ) | ( buffer.Array[ buffer.Offset + 1 ] );
            
            result = new H265NalUnit();

            result.ForbiddenBit  = (byte)        ( ( header >> 15) & 0x01 ) == 1;
            result.Type          = (H265NalUnitType) ( ( header >> 9 ) & 0x3F );
            result.LayerId       = (byte)        ( ( header >> 3 ) & 0x3F );
            result.Tid           = (byte)        (   header        & 0x07 );

            if ( buffer.Count > 2 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 2 , buffer.Array.Length - ( buffer.Offset + 2 ) );
            }

            return true;
        }

        public static IList<ArraySegment<byte>> ParseAggregates( ArraySegment<byte> buffer )
        {
            var results = new List<ArraySegment<byte>>();

            for ( var index = 2 ; index < buffer.Count ; )
            {
                var size = buffer.Array[ buffer.Offset + index++ ] * 0x100 | buffer.Array[ buffer.Offset + index ];

                var delta = buffer.Count - index++;

                if ( 0 < size && size < delta )
                {
                    results.Add( new ArraySegment<byte>( buffer.Array , index , size ) );

                    index += size;
                }
            }

            return results;
        }
    } 
}