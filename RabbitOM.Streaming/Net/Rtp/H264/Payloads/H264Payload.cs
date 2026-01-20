using System;

namespace RabbitOM.Streaming.Net.Rtp.H264.Payloads
{
    public struct H264Payload
    {
        public H264PayloadType Type { get; private set; }



        public static bool IsSlice( in H264Payload payload )
            => payload.Type >= H264PayloadType.SINGLE_SLICE && payload.Type <= H264PayloadType.SINGLE_RESERVED_K;



        public static bool TryParse( in ArraySegment<byte> buffer , out H264Payload result )
        {
            result = default;

            if ( buffer.Count < 1 )
            {
                return false;
            }

            result = new H264Payload()
            { 
                Type = (H264PayloadType) ( buffer.Array[ buffer.Offset ] & 0x1F ) 
            };

            return true;
        }
    } 
}