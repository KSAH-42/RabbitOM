using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class CSeqRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "CSeq";



        
        public long Value { get; set; }




        public override string ToString()
        {
            return Value.ToString();
        }




        public static bool TryParse( string input , out CSeqRtspHeaderValue result )
        {
            result = long.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , out var value )
                ? new CSeqRtspHeaderValue() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
