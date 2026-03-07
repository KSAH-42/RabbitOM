using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Filters;

    public sealed class MaxForwardsRtspHeader
    {
        public static readonly string TypeName = "Max-Forwards";

        
        public long Value { get; set; }


        public override string ToString()
        {
            return Value.ToString();
        }


        public static bool TryParse( string input , out MaxForwardsRtspHeader result )
        {
            result = long.TryParse( StringRtspHeaderFilter.UnQuoteFilter.Filter( input ) , out long value ) ? new MaxForwardsRtspHeader() { Value = value } : null;

            return result != null;
        }
    }
}
