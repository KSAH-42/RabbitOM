using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    public struct H264NalUnit
    {
        public bool ForbiddenBit { get; private set; }
        public H264NalUnitType Type { get; private set; }
        public byte Nri { get; private set; }
        public ArraySegment<byte> Payload { get; private set; }
        


        


        

        public static bool IsInvalidOrUnDefined( ref H264NalUnit nalUnit )
        {
            return nalUnit.Type == H264NalUnitType.UNKNOWN;
        }

        public static bool IsSingle( ref H264NalUnit nalUnit )
        {
            return H264NalUnitType.SINGLE_SLICE <= nalUnit.Type && nalUnit.Type <= H264NalUnitType.SINGLE_RESERVED_K;
        }






        // https://datatracker.ietf.org/doc/html/rfc6184#section-1.3
        
        //      header            payload can be null
        //  +---------------+  +-----------------------+
        //  |7|6|5|4|3|2|1|0|  |                       |
        //  +-+-+-+-+-+-+-+-+  |                       |
        //  |F|NRI|  Type   |  |                       |
        //  +---------------+  +-----------------------+
        
        



        public static bool TryParse( ArraySegment<byte> buffer , out H264NalUnit result )
        {
            result = default;

            if ( buffer.Count < 1 )
            {
                return false;
            }

            var header = buffer.Array[ buffer.Offset ];
            
            result = new H264NalUnit();

            result.Type          = (H264NalUnitType) ( header  & 0x1F );
            result.ForbiddenBit  = (byte) ( ( header >> 7 ) & 0x01 ) == 1;
            result.Nri           = (byte) ( ( header >> 5 ) & 0x03 );

            if ( buffer.Count > 1 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 1 , buffer.Array.Length - ( buffer.Offset + 1 ) );
            }

            return true;
        }

        public static IList<ArraySegment<byte>> ParseAggregates( ArraySegment<byte> buffer )
        {
            var results = new List<ArraySegment<byte>>();

            var index = buffer.Offset + 1;
            
            while ( index < buffer.Array.Length - 2 )
            {
                var size = buffer.Array[ index ++ ] * 0x100 | buffer.Array[ index ++ ];

                if ( 0 < size && size <= (buffer.Array.Length - (buffer.Offset + index)) )
                {
                    results.Add( new ArraySegment<byte>( buffer.Array , index , size ) );
                }

                index += size ;
            }

            return results;
        }
    } 
}