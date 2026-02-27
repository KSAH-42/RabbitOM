using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ContentLengthRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Content-Length";



        
        
        public long Value { get; set; }





        public override string ToString()
        {
            return Value.ToString();
        }




        public static bool TryParse( string input , out ContentLengthRtspHeaderValue result )
        {
            result = long.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , out var value )
                ? new ContentLengthRtspHeaderValue() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
