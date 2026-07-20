using System;

namespace RabbitOM.Streaming.Rtp.H265.Payloads.Types
{
    public struct H265NalUnit
    {
        public bool ForbiddenBit { get; private set; }

        public byte Type { get; private set; }

        public byte LayerId { get; private set; }

        public byte TemportalId { get; private set; }

        public ArraySegment<byte> Payload { get; private set; }




        public static bool IsNullOrForbidden( ArraySegment<byte> buffer )
        {
            return buffer.Count <= 0 || ( ( buffer.Array[ buffer.Offset ] >> 7 ) & 0x01 ) == 1;
        }



        public static bool TryParse( ArraySegment<byte> buffer , out H265NalUnit result )
        {
            result = default;

            if ( buffer.Count < 2 )
            {
                return false;
            }

            var header = ( buffer.Array[ buffer.Offset ] << 8 ) | ( buffer.Array[ buffer.Offset + 1 ] );

            result = new H265NalUnit();

            result.ForbiddenBit  = (byte) ( ( header >> 15) & 0x01 ) == 1;
            result.Type          = (byte) ( ( header >> 9 ) & 0x3F );
            result.LayerId       = (byte) ( ( header >> 3 ) & 0x3F );
            result.TemportalId   = (byte) (   header        & 0x07 );

            if ( buffer.Count > 2 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 2 , buffer.Count - 2 );
            }

            return true;
        }
    }
}