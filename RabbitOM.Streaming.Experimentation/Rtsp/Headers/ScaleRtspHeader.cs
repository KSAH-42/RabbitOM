using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ScaleRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Scale";



        
        public float Value { get; set; }




        public override string ToString()
        {
            return RtspHeaderParser.Formatter.Format( Value );
        }





        public static bool TryParse( string input , out ScaleRtspHeader result )
        {
            result = RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , out float value )
                ? new ScaleRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
