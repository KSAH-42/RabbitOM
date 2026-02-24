using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ContentLengthRtspHeader
    {
        public static readonly string TypeName = "Content-Length";



        
        
        public long Value { get; set; }





        public override string ToString()
        {
            return Value.ToString();
        }




        public static bool TryParse( string input , out ContentLengthRtspHeader result )
        {
            result = long.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , out var value )
                ? new ContentLengthRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
