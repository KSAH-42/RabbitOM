using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class ContentLengthRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Content-Length";        
        
        public long Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static bool TryParse( string input , out ContentLengthRtspHeader result )
        {
            result = LongRtspHeaderParser.TryParse( input , out long value ) ? new ContentLengthRtspHeader() { Value = value } : null ;

            return result != null;
        }
    }
}
