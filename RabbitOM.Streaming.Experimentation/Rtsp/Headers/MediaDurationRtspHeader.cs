using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Filters;
using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class MediaDurationRtspHeader
    {
        public static readonly string TypeName = "Media-Duration";

        public double Value { get; set; }

        public override string ToString()
        {
            return Value.ToString( "F3" , CultureInfo.InvariantCulture );
        }

        public static bool TryParse( string input , out MediaDurationRtspHeader result )
        {
            result = double.TryParse( StringRtspHeaderFilter.UnQuoteFilter.Filter( input ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var value )
                    ? new MediaDurationRtspHeader() { Value = value } 
                    : null
                    ;

            return result != null;
        }
    }
}
