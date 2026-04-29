using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Compliances;
using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class MediaDurationRtspHeaderValue
    {
        public double Value { get; set; }

        public static bool TryParse( string input , out MediaDurationRtspHeaderValue result )
        {
            result = double.TryParse( StringValueNormalizer.TrimWithUnQuoteNormalizer.Normalize( input ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var value ) ? new MediaDurationRtspHeaderValue() { Value = value } : null;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString( "F3" , CultureInfo.InvariantCulture );
        }
    }
}
