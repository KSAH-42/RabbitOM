using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ExpiresRtspHeader
    {
        public static readonly string TypeName = "Expires";




        
        public DateTime Value { get; set; }





        public override string ToString()
        {
            return DateTimeRtspHeaderParser.Format( Value );
        }





        public static bool TryParse( string input , out ExpiresRtspHeader result )
        {
            result = DateTimeRtspHeaderParser.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , out DateTime value )
                ? new ExpiresRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
