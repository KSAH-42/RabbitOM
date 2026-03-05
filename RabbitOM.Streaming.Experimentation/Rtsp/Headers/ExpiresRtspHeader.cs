using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class ExpiresRtspHeader : RtspHeader
    {
        public static string TypeName { get; } = "Expires";

        public DateTime Value { get; set; }

        public override string ToString()
        {
            return DateTimeRtspHeaderParser.Format( Value );
        }

        public static bool TryParse( string input , out ExpiresRtspHeader result )
        {
            result = DateTimeRtspHeaderParser.TryParse( input , out DateTime value ) ? new ExpiresRtspHeader() { Value = value } : null;

            return result != null;
        }
    }
}
