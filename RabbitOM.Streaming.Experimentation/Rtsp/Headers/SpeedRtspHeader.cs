using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class SpeedRtspHeader
    {
        public static readonly string TypeName = "Speed";
        
        public long Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static bool TryParse( string input , out SpeedRtspHeader result )
        {
            result = long.TryParse( StringValueAdapter.UnQuoteAdapter.Adapt( input ) , out long value ) ? new SpeedRtspHeader() { Value = value } : null ;

            return result != null;
        }
    }
}
