using System;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved
{
    using RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved.Adapters;

    public sealed class LastModifiedRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Last-Modified";
        
        public DateTime Value { get; set; }

        public static bool TryParse( string input , out LastModifiedRtspHeader result )
        {
            result = DateTime.TryParse( StringValueAdapter.TrimWithUnQuoteAdapter.Adapt( input ) , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out var value ) 
                ? new LastModifiedRtspHeader() { Value = value } 
                : null;

            return result != null;
        }

        public override string ToString()
        {
            return ( Value.Kind == DateTimeKind.Local ? Value.ToUniversalTime() : Value ).ToString( "r" , CultureInfo.InvariantCulture);
        }
    }
}
