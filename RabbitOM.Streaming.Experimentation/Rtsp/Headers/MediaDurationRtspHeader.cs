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
            return string.Format( CultureInfo.InvariantCulture , "{0:F3}" , Value );
        }





        public static bool TryParse( string input , out MediaDurationRtspHeader result )
        {
            result = double.TryParse( StringRtspHeaderNormalizer.Normalize( input?.Replace( "," , "." ) ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var value )
                ? new MediaDurationRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
