using System;
using System.Globalization;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    public sealed class SpeedRtspHeaderValue
    {
        public double Value { get; set; }

        public override string ToString()
        {
            return Value.ToString( "F1" , CultureInfo.InvariantCulture );
        }

        public static bool TryParse( string input , out SpeedRtspHeaderValue result )
        {
            result = double.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( input ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture  , out var value ) ? new SpeedRtspHeaderValue() { Value = value } : null ;

            return result != null;
        }
    }
}
