using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Compliances;

    public sealed class DateTimeRtspHeaderValue
    {
        public DateTime Value { get; set; }

        public static bool TryParse( string input , out DateTimeRtspHeaderValue result )
        {
            result = DateTime.TryParse( StringValueNormalizer.TrimWithUnQuoteNormalizer.Normalize( input ) , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out var value ) 
                ? new DateTimeRtspHeaderValue() { Value = value } 
                : null;

            return result != null;
        }

        public override string ToString()
        {
            return ( Value.Kind == DateTimeKind.Local ? Value.ToUniversalTime() : Value ).ToString( "r" , CultureInfo.InvariantCulture);
        }
    }
}
