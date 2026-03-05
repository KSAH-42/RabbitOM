using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class DateRtspHeader : RtspHeader
    {
        public static string TypeName { get; } = "Date";
        
        public DateTime Value { get; set; }

        public override string ToString()
        {
            return DateTimeRtspHeaderParser.Format( Value );
        }

        public static bool TryParse( string input , out DateRtspHeader result )
        {
            result = DateTimeRtspHeaderParser.TryParse( input , out DateTime value ) ? new DateRtspHeader() { Value = value } : null;

            return result != null;
        }
    }
}
