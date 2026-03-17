using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class LongRtspHeaderValue
    {
        public long Value { get; set; }

        public static bool TryParse( string input , out LongRtspHeaderValue result )
        {
            result = long.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ) , out var value ) ? new LongRtspHeaderValue() { Value = value } : null;
            
            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
