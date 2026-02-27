using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class LastModifiedRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Last-Modified";



        
        public DateTime Value { get; set; }




        public override string ToString()
        {
            return RtspHeaderParser.Format( Value );
        }




        public static bool TryParse( string input , out LastModifiedRtspHeaderValue result )
        {
            result = RtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , out DateTime value )
                ? new LastModifiedRtspHeaderValue() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
