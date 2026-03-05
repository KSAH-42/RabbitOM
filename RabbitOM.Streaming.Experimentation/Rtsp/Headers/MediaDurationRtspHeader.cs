using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class MediaDurationRtspHeader : RtspHeader
    {
        public static string TypeName { get; } = "Media-Duration";



        
        public double Value { get; set; }




        public override string ToString()
        {
            return DoubleRtspHeaderParser.Format( Value );
        }



        public static bool TryParse( string input , out MediaDurationRtspHeader result )
        {
            result = DoubleRtspHeaderParser.TryParse( input , out double value )
                ? new MediaDurationRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
