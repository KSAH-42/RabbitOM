using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265NalUnit
    {
        public bool ForbiddenBit { get; set; }
        
        public NalUnitType Type { get; set; }
        
        public byte LayerId { get; set; }
        
        public byte TemporalId { get; set; }
        
        public ArraySegment<byte> Payload { get; set; }





        public bool TryValidate()
        {
            return Type == NalUnitType.UNDEFINED || Type >= NalUnitType.INVALID;
        }

        public IList<ArraySegment<byte>> GetAggregationUnits()
        {
            var results = new List<ArraySegment<byte>>( 100 );

            if ( Payload.Count > 2 )
            {
                int index = 0;

                while ( index < Payload.Count - 2 )
                {
                    int size = Payload.Array[ Payload.Offset + index ++ ] * 0x100 
                             | Payload.Array[ Payload.Offset + index ];

                    int delta = Payload.Count - index ++ ;

                    if ( 0 < size && size < delta )
                    {
                        results.Add( new ArraySegment<byte>( Payload.Array , index , size ) );

                        index += size;
                    }
                }
            }

            return results;
        }






        public static bool TryParse( ArraySegment<byte> buffer , out H265NalUnit result )
        {
            result = null;

            if ( buffer.Count <= 2 )
            {
                return false;
            }

            int header = ( buffer.Array[ buffer.Offset ] << 8 ) | ( buffer.Array[ buffer.Offset + 1 ] );

            result = new H265NalUnit();

            result.TemporalId    = (byte)        ( ( header      ) & 0x07 );
            result.LayerId       = (byte)        ( ( header >> 3 ) & 0x1F );
            result.LayerId      |= (byte)        ( ( header >> 8 ) & 0x01 );
            result.Type          = (NalUnitType) ( ( header >> 9 ) & 0x3F );
            result.ForbiddenBit  = (byte)        ( ( header >> 15) & 0x1  ) == 1;

            result.Payload = new ArraySegment<byte>(  buffer.Array , buffer.Offset + 2 , buffer.Count - 2 );
            
            return true;
        }
    } 
}