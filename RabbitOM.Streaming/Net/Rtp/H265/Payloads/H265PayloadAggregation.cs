using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H265.Payloads
{
    public struct H265PayloadAggregation
    {
        public IReadOnlyCollection<ArraySegment<byte>> NalUnits { get; private set; }
        
        public static H265PayloadAggregation Parse( in ArraySegment<byte> buffer , bool donl )
        {
            var nalUnits = new List<ArraySegment<byte>>();

            var index = buffer.Offset + 2;
            
            while ( ( index += ( donl ? 2 : 0 ) ) < buffer.Array.Length - 2 )
            {
                var size = buffer.Array[ index ++ ] * 0x100 | buffer.Array[ index ++ ];

                if ( 0 < size && size <= (buffer.Array.Length - (buffer.Offset + index)) )
                {
                    nalUnits.Add( new ArraySegment<byte>( buffer.Array , index , size ) );
                }

                index += size;
            }

            return new H265PayloadAggregation() { NalUnits = nalUnits };
        }       
    } 
}