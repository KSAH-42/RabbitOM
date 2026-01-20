using System;

namespace RabbitOM.Streaming.Net.Rtp.H264.Payloads.Entities
{
    public struct H264NalUnit
    {
        public bool ForbiddenBit { get; private set; }
        public byte Type { get; private set; }
        public byte Nri { get; private set; }
        public ArraySegment<byte> Payload { get; private set; }
        



        public static bool IsNullOrForbidden( in ArraySegment<byte> buffer )
            => buffer.Count <= 0 || ( ( buffer.Array[ buffer.Offset ] >> 7 ) & 0x01 ) == 1;
        


       
        public static bool TryParse( in ArraySegment<byte> buffer , out H264NalUnit result )
        {
            result = default;

            if ( buffer.Count < 1 )
            {
                return false;
            }

            result = new H264NalUnit();

            result.Type          = (byte) (   buffer.Array[ buffer.Offset ]        & 0x1F );
            result.ForbiddenBit  = (byte) ( ( buffer.Array[ buffer.Offset ] >> 7 ) & 0x01 ) == 1;
            result.Nri           = (byte) ( ( buffer.Array[ buffer.Offset ] >> 5 ) & 0x03 );

            if ( buffer.Count > 1 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 1 , buffer.Count - 1 );
            }

            return true;
        }
    } 
}