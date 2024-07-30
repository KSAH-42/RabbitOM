using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265NalUnit
    {
        private static readonly byte[] StartPrefixS0   = { };
        private static readonly byte[] StartPrefixS3   = { 0x00 , 0x00 , 0x01 };
        private static readonly byte[] StartPrefixS4   = { 0x00 , 0x00 , 0x00 , 0x01 };



        public bool ForbiddenBit { get; set; }
        
        public NalUnitType Type { get; set; }
        
        public byte LayerId { get; set; }
        
        public byte TID { get; set; }
        
        public ArraySegment<byte> Payload { get; set; }



        public bool TryValidate()
        {
            return Payload != null && Payload.Count >= 1;
        }

        // TODO: to be removed
        public override string ToString()
        {
            return $"{(byte)Type} {ForbiddenBit} {LayerId} {TID} {Payload.Count}";
        }



        /*
           Please note that from the rfc, bit order is not like 7 6 5 4 3 2 1 0 | 7 6 5 4 3 2 1 0 

            +---------------+---------------+
            |0|1|2|3|4|5|6|7|0|1|2|3|4|5|6|7|
            +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            |F|   Type    |  LayerId  | TID |
            +-------------+-----------------+
         */


        public static bool TryParse( ArraySegment<byte> buffer , out H265NalUnit result )
        {
            result = default;

            if ( buffer.Count <= StartPrefixS4.Length )
            {
                return false;
            }

            var prefix = buffer.StartsWith( StartPrefixS3 ) ? StartPrefixS3
                       : buffer.StartsWith( StartPrefixS4 ) ? StartPrefixS4
                       : StartPrefixS0;
                       ;

            if ( (buffer.Count - (prefix.Length+2) ) <= 0 )
            {
                return false;
            }

            int index = prefix.Length;

            int header = ( buffer.Array[ buffer.Offset + index ] << 8 ) | ( buffer.Array[ buffer.Offset + ++ index ] );

            result = new H265NalUnit();

            result.TID           = (byte)        ( ( header      ) & 0x07 );
            result.LayerId       = (byte)        ( ( header >> 3 ) & 0x1F );
            result.LayerId      |= (byte)        ( ( header >> 8 ) & 0x01 );
            result.Type          = (NalUnitType) ( ( header >> 9 ) & 0x3F );
            result.ForbiddenBit  = (byte)        ( ( header >> 15) & 0x1  ) == 1;

            result.Payload = new ArraySegment<byte>(  buffer.Array , buffer.Offset + ++ index , buffer.Count - index );
            
            return true;
        }
    } 
}