using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H264.Payloads
{
    public struct H264PayloadStapA
    {
        public IReadOnlyCollection<ArraySegment<byte>> NalUnits { get; private set; }

        public static H264PayloadStapA Parse( in ArraySegment<byte> buffer )
        {
            var units = new List<ArraySegment<byte>>();

            var index = buffer.Offset + 1;
            
            while ( index < buffer.Array.Length - 2 )
            {
                var size = buffer.Array[ index ++ ] * 0x100 | buffer.Array[ index ++ ];

                if ( 0 < size && size <= (buffer.Array.Length - (buffer.Offset + index)) )
                {
                    units.Add( new ArraySegment<byte>( buffer.Array , index , size ) );
                }

                index += size ;
            }

            return new H264PayloadStapA() { NalUnits = units };
        }
    } 
}