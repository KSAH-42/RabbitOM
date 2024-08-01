using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265NalUnit
    {
        public bool ForbiddenBit { get; set; }
        
        public NalUnitType Type { get; set; }
        
        public byte LayerId { get; set; }
        
        public byte TemporalId { get; set; }
        
        public ArraySegment<byte> Payload { get; set; }



        
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