using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class MaxForwardsRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Max-Forwards";



        
        public long Value { get; set; }




        public override string ToString()
        {
            return Value.ToString();
        }




        public static bool TryParse( string input , out MaxForwardsRtspHeader result )
        {
            result = long.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , out var value )
                ? new MaxForwardsRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
