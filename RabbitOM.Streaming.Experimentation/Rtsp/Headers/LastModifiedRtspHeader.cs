using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Filters;

    public sealed class LastModifiedRtspHeader
    {
        public static readonly string TypeName = "Last-Modified";
        
        public DateTime Value { get; set; }

        public override string ToString()
        {
            var value = Value.Kind == DateTimeKind.Local ? Value.ToUniversalTime() : Value;

            return value.ToString( "r" , CultureInfo.InvariantCulture );
        }

        public static bool TryParse( string input , out LastModifiedRtspHeader result )
        {
            result = DateTime.TryParse( StringRtspHeaderFilter.UnQuoteFilter.Filter( input ) , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out var value ) 
                ? new LastModifiedRtspHeader() { Value = value } 
                : null
                ;

            return result != null;
        }
    }
}
