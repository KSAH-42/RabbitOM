using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class MaxForwardsRtspHeader
    {
        public static readonly string TypeName = "Max-Forwards";
        
        public long Value { get; set; }

        public static bool TryParse( string input , out MaxForwardsRtspHeader result )
        {
            result = long.TryParse( RtspValueNormalizer.Normalize( input ) , out var value )
                ? new MaxForwardsRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
