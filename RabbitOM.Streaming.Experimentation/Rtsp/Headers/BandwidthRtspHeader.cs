using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class BandwidthRtspHeader
    {
        public static readonly string TypeName = "Bandwidth";
       
        
        public long Value { get; set; }
       
        
        public override string ToString()
        {
            return Value.ToString();
        }

                
        public static bool TryParse( string input , out BandwidthRtspHeader result )
        {
            result = long.TryParse( StringValueAdapter.UnQuoteAdapter.Adapt( input ) , out long value ) ? new BandwidthRtspHeader() { Value = value } : null ;

            return result != null;
        }
    }
}
