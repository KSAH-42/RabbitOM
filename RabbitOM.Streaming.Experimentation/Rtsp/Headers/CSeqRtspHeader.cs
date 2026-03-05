using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class CSeqRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "CSeq";

        public long Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static bool TryParse( string input , out CSeqRtspHeader result )
        {
            result = LongRtspHeaderParser.TryParse( input , out long value ) ? new CSeqRtspHeader() { Value = value } : null;

            return result != null;
        }
    }
}
