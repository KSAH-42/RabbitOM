using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class MaxForwardsRtspHeaderValue
    {
        public uint Value { get; set; }

        public static bool TryParse( string input , out MaxForwardsRtspHeaderValue result )
        {
            result = uint.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( input ) , out var value ) ? new MaxForwardsRtspHeaderValue() { Value = value } : null;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
