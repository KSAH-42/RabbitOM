using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class MediaDurationRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Media-Duration";



        
        public double Value { get; set; }




        public override string ToString()
        {
            return RtspHeaderParser.Formatter.Format( Value );
        }



        public static bool TryParse( string input , out MediaDurationRtspHeader result )
        {
            result = RtspHeaderParser.TryParse( RtspHeaderParser.Formatter.Filter( input ) , out double value )
                ? new MediaDurationRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
