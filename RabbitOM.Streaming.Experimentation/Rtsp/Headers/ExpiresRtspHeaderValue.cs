using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ExpiresRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Expires";




        
        public DateTime Value { get; set; }





        public override string ToString()
        {
            return RtspHeaderParser.Format( Value );
        }





        public static bool TryParse( string input , out ExpiresRtspHeaderValue result )
        {
            result = RtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , out DateTime value )
                ? new ExpiresRtspHeaderValue() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
