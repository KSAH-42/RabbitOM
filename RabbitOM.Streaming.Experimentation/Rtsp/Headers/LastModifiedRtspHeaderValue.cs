using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class LastModifiedRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Last-Modified";
        
        public DateTime Value { get; set; }

        public static bool TryParse( string input , out LastModifiedRtspHeaderValue result )
        {
            result = DateTime.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ) , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out var value ) 
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
