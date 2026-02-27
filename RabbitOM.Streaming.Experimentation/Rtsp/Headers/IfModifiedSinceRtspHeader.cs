using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class IfModifiedSinceRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "If-Modified-Since";



        
        public DateTime Value { get; set; }




        public override string ToString()
        {
            return RtspHeaderParser.Format( Value );
        }




        public static bool TryParse( string input , out IfModifiedSinceRtspHeader result )
        {
            result = RtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , out DateTime value )
                ? new IfModifiedSinceRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
