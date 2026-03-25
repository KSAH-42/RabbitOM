using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;

    public sealed class LastModifiedRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Last-Modified";
        
        public DateTime Value { get; set; }

        public static implicit operator LastModifiedRtspHeaderValue( DateTime value )
        {
            return new LastModifiedRtspHeaderValue() { Value = value };
        }

        public static bool TryParse( string input , out LastModifiedRtspHeaderValue result )
        {
            result = DateTime.TryParse( StringValueNormalizer.TrimWithUnQuoteNormalizer.Normalize( input ) , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out var value ) 
                ? new LastModifiedRtspHeaderValue() { Value = value } 
                : null;

            return result != null;
        }

        public override string ToString()
        {
            return ( Value.Kind == DateTimeKind.Local ? Value.ToUniversalTime() : Value ).ToString( "r" , CultureInfo.InvariantCulture);
        }
    }
}
