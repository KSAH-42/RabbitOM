using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers
{
    using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers.Normalizers;

    public sealed class BandwidthRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Bandwidth";
        
        public uint Value { get; set; }
                
        public static bool TryParse( string input , out BandwidthRtspHeaderValue result )
        {
            result = uint.TryParse( StringValueNormalizer.TrimWithUnQuoteNormalizer.Normalize( input ) , out var value ) ? new BandwidthRtspHeaderValue() { Value = value } : null ;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
