using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class DateRtspHeader
    {
        public static readonly string TypeName = "Date";
        
        public DateTime Value { get; set; }

        public static bool TryParse( string input , out DateRtspHeader result )
        {
            result = DateTimeRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( input ) , out var value )
                ? new DateRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }

        public override string ToString()
        {
            return DateTimeRtspHeaderParser.Format( Value );
        }
    }
}
