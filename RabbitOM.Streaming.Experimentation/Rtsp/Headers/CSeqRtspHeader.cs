using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class CSeqRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "CSeq";

        public uint Value { get; set; }

        public static bool TryParse( string input , out CSeqRtspHeader result )
        {
            result = uint.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ) , out var value ) ? new CSeqRtspHeader() { Value = value } : null;
            
            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
