using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class ContentLengthRtspHeader
    {
        public static readonly string TypeName = "Content-Length";
        
        public long Value { get; set; }

        public static bool TryParse( string input , out ContentLengthRtspHeader result )
        {
            result = long.TryParse( RtspValueNormalizer.Normalize( input ) , out var value )
                ? new ContentLengthRtspHeader() { Value = value }
                : null
                ;

            return result != null;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
