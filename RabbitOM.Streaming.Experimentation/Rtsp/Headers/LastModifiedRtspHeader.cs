using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class LastModifiedRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Last-Modified";
        
        public DateTime Value { get; set; }

        public override string ToString()
        {
            return DateTimeRtspHeaderParser.Format( Value );
        }

        public static bool TryParse( string input , out LastModifiedRtspHeader result )
        {
            result = DateTimeRtspHeaderParser.TryParse( input , out DateTime value ) ? new LastModifiedRtspHeader() { Value = value } : null;

            return result != null;
        }
    }
}
