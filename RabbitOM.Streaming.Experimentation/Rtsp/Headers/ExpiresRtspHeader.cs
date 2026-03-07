using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class ExpiresRtspHeader
    {
        public static readonly string TypeName = "Expires";

        public DateTime Value { get; set; }

        public override string ToString()
        {
            var value = Value.Kind == DateTimeKind.Local ? Value.ToUniversalTime() : Value;

            return value.ToString( "r" , CultureInfo.InvariantCulture );
        }

        public static bool TryParse( string input , out ExpiresRtspHeader result )
        {
            result = DateTime.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ) , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out var value ) 
                ? new ExpiresRtspHeader() { Value = value } 
                : null
                ;

            return result != null;
        }
    }
}
