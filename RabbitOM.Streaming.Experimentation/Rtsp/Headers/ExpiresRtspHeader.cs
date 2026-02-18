using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class ExpiresRtspHeader
    {
        public static readonly string TypeName = "Expires";
        
        public DateTime Value { get; set; }

        public static bool TryParse( string input , out ExpiresRtspHeader result )
        {
            result = DateTimeRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( input ) , out var value )
                ? new ExpiresRtspHeader() { Value = value }
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
