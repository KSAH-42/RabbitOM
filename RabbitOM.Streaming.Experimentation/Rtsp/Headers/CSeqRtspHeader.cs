using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class CSeqRtspHeader
    {
        public static readonly string TypeName = "CSeq";

        public long Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static bool TryParse( string input , out CSeqRtspHeader result )
        {
            result = long.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ) , out long value ) ? new CSeqRtspHeader() { Value = value } : null;

            return result != null;
        }
    }
}
