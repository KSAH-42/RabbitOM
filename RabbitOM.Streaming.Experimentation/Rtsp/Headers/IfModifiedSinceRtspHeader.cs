using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class IfModifiedSinceRtspHeader : RtspHeader
    {
        public static string TypeName { get; } = "If-Modified-Since";
        
        public DateTime Value { get; set; }

        public override string ToString()
        {
            return DateTimeRtspHeaderParser.Format( Value );
        }

        public static bool TryParse( string input , out IfModifiedSinceRtspHeader result )
        {
            result = DateTimeRtspHeaderParser.TryParse( input , out DateTime value ) ? new IfModifiedSinceRtspHeader() { Value = value } : null;

            return result != null;
        }
    }
}
