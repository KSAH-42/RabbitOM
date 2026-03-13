using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class BandwidthRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Bandwidth";
        
        public uint Value { get; set; }
                
        public static bool TryParse( string input , out BandwidthRtspHeader result )
        {
            result = uint.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ) , out var value ) ? new BandwidthRtspHeader() { Value = value } : null ;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
