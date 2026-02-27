using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ExpiresRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Expires";




        
        public DateTime Value { get; set; }





        public override string ToString()
        {
            return RtspHeaderParser.Format( Value );
        }





        public static bool TryParse( string input , out ExpiresRtspHeader result )
        {
            result = RtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , out DateTime value )
                ? new ExpiresRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
