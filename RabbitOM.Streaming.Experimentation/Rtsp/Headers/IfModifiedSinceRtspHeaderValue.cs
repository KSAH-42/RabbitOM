using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class IfModifiedSinceRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "If-Modified-Since";



        
        public DateTime Value { get; set; }




        public override string ToString()
        {
            return DateTimeRtspHeaderParser.Format( Value );
        }




        public static bool TryParse( string input , out IfModifiedSinceRtspHeaderValue result )
        {
            result = DateTimeRtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , out DateTime value )
                ? new IfModifiedSinceRtspHeaderValue() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
