using System;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265NalUnit
    {
        private static readonly byte[] StartPrefixS0   = { };
        private static readonly byte[] StartPrefixS3   = { 0x00 , 0x00 , 0x01 };
        private static readonly byte[] StartPrefixS4   = { 0x00 , 0x00 , 0x00 , 0x01 };



        public ArraySegment<byte> Prefix { get; set; }

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
            return $"{Prefix.Count} {ForbiddenBit} {Type} {LayerId} {TID} {Payload.Count}";
        }



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

            int index = prefix.Length;

            result = new H265NalUnit()
            {
                Prefix = new ArraySegment<byte>( prefix , 0 , prefix.Length ),
            };

            result.ForbiddenBit  = (byte)        ( ( buffer.Array[ buffer.Offset + index ] >> 7 ) & 0x1  ) == 1;
            result.Type          = (NalUnitType) ( ( buffer.Array[ buffer.Offset + index ] >> 1 ) & 0x3F );
            result.LayerId       = (byte)        ( ( buffer.Array[ buffer.Offset + index ] << 7 ) & 0x80 );
            result.LayerId      |= (byte)        ( ( buffer.Array[ buffer.Offset + index ] >> 3 ) & 0x1F );
            result.TID           = (byte)        ( ( buffer.Array[ buffer.Offset + index ]      ) & 0x03 );

            result.Payload = new ArraySegment<byte>(  buffer.Array , buffer.Offset + ++ index , buffer.Count - index );
            
            return true;
        }
    } 
}