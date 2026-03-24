using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class IfModifiedSinceRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "If-Modified-Since";
        
        public DateTime Value { get; set; }
        
        public static implicit operator IfModifiedSinceRtspHeaderValue( DateTime value )
        {
            return new IfModifiedSinceRtspHeaderValue() { Value = value };
        }

        public override string ToString()
        {
            var value = Value.Kind == DateTimeKind.Local ? Value.ToUniversalTime() : Value;

            return value.ToString( "r" , CultureInfo.InvariantCulture );
        }

        public static bool TryParse( string input , out IfModifiedSinceRtspHeaderValue result )
        {
            result = DateTime.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ) , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out var value ) 
                ? new IfModifiedSinceRtspHeaderValue() { Value = value } 
                : null
                ;

            return result != null;
        }
    }
}
