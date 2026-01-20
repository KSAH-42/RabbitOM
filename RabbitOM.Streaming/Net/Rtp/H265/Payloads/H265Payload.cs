using System;

namespace RabbitOM.Streaming.Net.Rtp.H265.Payloads
{
    public struct H265Payload
    {
        public H265PayloadType Type { get; private set; }

        public static bool IsSlice( in H265Payload payload )
            => payload.Type >= H265PayloadType.CODED_SLICE_TRAIL_N && payload.Type <= H265PayloadType.CODED_SLICE_CRA;

        public static bool TryParse( in ArraySegment<byte> buffer , out H265Payload result )
        {
            result = default;

            if ( buffer.Count < 1 )
            {
                return false;
            }
            
            result = new H265Payload()
            { 
                Type = (H265PayloadType) ( ( buffer.Array[ buffer.Offset ] >> 1 ) & 0x3F )
            };

            return true;
        }
    } 
}