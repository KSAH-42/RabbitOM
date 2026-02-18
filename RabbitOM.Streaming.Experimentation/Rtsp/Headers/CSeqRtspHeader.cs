using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class CSeqRtspHeader
    {
        public static readonly string TypeName = "CSeq";
        
        public long Value { get; set; }

        public static bool TryParse( string input , out CSeqRtspHeader result )
        {
            result = long.TryParse( RtspValueNormalizer.Normalize( input ) , out var value )
                ? new CSeqRtspHeader() { Value = value }
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
