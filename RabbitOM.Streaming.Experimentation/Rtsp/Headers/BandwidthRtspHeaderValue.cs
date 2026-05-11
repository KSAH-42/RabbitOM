using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class BandwidthRtspHeaderValue
    {
        public uint Value { get; set; }
                
        public static bool TryParse( string input , out BandwidthRtspHeaderValue result )
        {
            result = uint.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( input ) , out var value ) ? new BandwidthRtspHeaderValue() { Value = value } : null ;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
