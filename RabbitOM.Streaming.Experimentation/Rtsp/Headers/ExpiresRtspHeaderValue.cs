using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class ExpiresRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Expires";

        public DateTime Value { get; set; }

        public static implicit operator ExpiresRtspHeaderValue( DateTime value )
        {
            return new ExpiresRtspHeaderValue() { Value = value };
        }

        public static bool TryParse( string input , out ExpiresRtspHeaderValue result )
        {
            result = DateTime.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ) , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out var value ) 
                ? new ExpiresRtspHeaderValue() { Value = value } 
                : null;

            return result != null;
        }

        public override string ToString()
        {
            return ( Value.Kind == DateTimeKind.Local ? Value.ToUniversalTime() : Value ).ToString( "r" , CultureInfo.InvariantCulture);
        }
    }
}
