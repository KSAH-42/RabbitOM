using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class BandwithRtspHeader
    {
        public static readonly string TypeName = "Bandwith";
        
        public long Value { get; set; }

        public static bool TryParse( string input , out BandwithRtspHeader result )
        {
            result = long.TryParse( RtspValueNormalizer.Normalize( input ) , out var value )
                ? new BandwithRtspHeader() { Value = value }
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
