using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class ContentLengthRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Content-Length";        
        
        public long Value { get; set; }

        public static bool TryParse( string input , out ContentLengthRtspHeaderValue result )
        {
            result = long.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ) , out long value ) ? new ContentLengthRtspHeaderValue() { Value = value } : null ;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
