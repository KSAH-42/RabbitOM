using System;

namespace RabbitOM.Streaming.Rtp.H266.Payloads
{
    public struct H266Payload
    {
        public H266PayloadType Type { get; private set; }


        public static bool IsSlice( H266Payload payload )
        {
            return payload.Type >= H266PayloadType.TRAIL && payload.Type <= H266PayloadType.RSV_IRAP_11;
        }


        public static bool TryParse( ArraySegment<byte> buffer , out H266Payload result )
        {
            result = default;

            if ( buffer.Count < 2 )
            {
                return false;
            }

            result = new H266Payload() { Type = (H266PayloadType) ( ( buffer.Array[ buffer.Offset ] >> 3 ) & 0x3F ) };

            return true;
        }
    }
}
