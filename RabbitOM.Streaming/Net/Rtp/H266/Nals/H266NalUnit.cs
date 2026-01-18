using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H266.Nals
{
    public struct H266NalUnit
    {
        public bool ForbiddenBit { get; private set; }

        public bool ZBit { get; private set; }

        public byte LayerId { get; private set; }

        public H266NalUnitType Type { get; private set; }

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
            result.Type         = (H266NalUnitType) ( ( header >> 3 ) & 0x1F );
            result.LayerId      = (byte)            ( ( header >> 8 ) & 0x3F );
            result.TemporalId   = (byte)              ( header & 0x07 );

            if ( buffer.Count > 2 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 2 , buffer.Count - 2 );
            }

            return true;
        }

        public static bool TryParse( in ArraySegment<byte> buffer , out H266NalUnitType result )
        {
            result = H266NalUnitType.UNKNOWN;

            if ( buffer.Count < 2 )
            {
                return false;
            }

            result = (H266NalUnitType) ( ( buffer.Array[ buffer.Offset ] >> 3 ) & 0x3F );

            return true;
        }

        public static IList<ArraySegment<byte>> ParseAggregates( in ArraySegment<byte> buffer , bool donl )
        {
            var results = new List<ArraySegment<byte>>();

            var index = buffer.Offset + 2;
            
            while ( ( index += (donl ? 2 : 0 ) ) < buffer.Array.Length - 2 )
            {
                var size = buffer.Array[ index ++ ] * 0x100 | buffer.Array[ index ++ ];

                if ( 0 < size && size <= (buffer.Array.Length - (buffer.Offset + index)) )
                {
                    results.Add( new ArraySegment<byte>( buffer.Array , index , size ) );
                }

                index += size;
            }

            return results;
        }       
    }           
}
