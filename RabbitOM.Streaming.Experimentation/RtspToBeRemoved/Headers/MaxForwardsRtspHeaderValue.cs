using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers.Normalizers;
    public sealed class MaxForwardsRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Max-Forwards";
        
        public uint Value { get; set; }

        public static bool TryParse( string input , out MaxForwardsRtspHeaderValue result )
        {
            result = uint.TryParse( StringValueNormalizer.TrimWithUnQuoteNormalizer.Normalize( input ) , out var value ) ? new MaxForwardsRtspHeaderValue() { Value = value } : null;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
