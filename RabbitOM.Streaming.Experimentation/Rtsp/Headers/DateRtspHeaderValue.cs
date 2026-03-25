using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;

    public sealed class DateRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Date";
        
        public DateTime Value { get; set; }

        public static implicit operator DateRtspHeaderValue( DateTime value )
        {
            return new DateRtspHeaderValue() { Value = value };
        }

        public static bool TryParse( string input , out DateRtspHeaderValue result )
        {
            result = DateTime.TryParse( StringValueNormalizer.TrimWithUnQuoteNormalizer.Normalize( input ) , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out var value ) 
                ? new DateRtspHeaderValue() { Value = value } 
                : null;

            return result != null;
        }

        public override string ToString()
        {
            return ( Value.Kind == DateTimeKind.Local ? Value.ToUniversalTime() : Value ).ToString( "r" , CultureInfo.InvariantCulture);
        }
    }
}
