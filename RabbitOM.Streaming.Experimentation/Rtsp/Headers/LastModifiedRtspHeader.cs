using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class LastModifiedRtspHeader
    {
        public static readonly string TypeName = "Last-Modified";
        
        public DateTime Value { get; set; }

        public static bool TryParse( string input , out LastModifiedRtspHeader result )
        {
            result = DateTimeRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( input ) , out var value )
                ? new LastModifiedRtspHeader() { Value = value }
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
