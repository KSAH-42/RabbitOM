using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
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
            result = long.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , out var value )
                ? new CSeqRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
