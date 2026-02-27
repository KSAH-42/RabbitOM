using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class MediaDurationRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Media-Duration";



        
        public double Value { get; set; }




        public override string ToString()
        {
            return string.Format( CultureInfo.InvariantCulture , "{0:F3}" , Value );
        }





        public static bool TryParse( string input , out MediaDurationRtspHeaderValue result )
        {
            result = double.TryParse( StringRtspHeaderNormalizer.Normalize( input?.Replace( "," , "." ) ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var value )
                ? new MediaDurationRtspHeaderValue() { Value = value }
                : null
                ;

            return result != null;
        }
    }
}
