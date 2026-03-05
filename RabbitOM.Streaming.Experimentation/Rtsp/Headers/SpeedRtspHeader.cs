using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class SpeedRtspHeader : RtspHeader
    {
        public static string TypeName { get; } = "Speed";
        
        public long Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static bool TryParse( string input , out SpeedRtspHeader result )
        {
            result = LongRtspHeaderParser.TryParse( input , out long value ) ? new SpeedRtspHeader() { Value = value } : null ;

            return result != null;
        }
    }
}
