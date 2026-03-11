using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
    public sealed class MaxForwardsRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Max-Forwards";
        
        public long Value { get; set; }

        public static bool TryParse( string input , out MaxForwardsRtspHeader result )
        {
            result = long.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ) , out long value ) ? new MaxForwardsRtspHeader() { Value = value } : null;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
