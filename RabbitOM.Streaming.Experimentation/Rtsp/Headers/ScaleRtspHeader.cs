using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core;

    public sealed class ScaleRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Scale";
        
        public float Value { get; set; }

        public override string ToString()
        {
            return RtspHeaderParser.Format( Value );
        }

        public static bool TryParse( string input , out ScaleRtspHeader result )
        {
            result = RtspHeaderParser.TryParse( input , out float value ) ? new ScaleRtspHeader() { Value = value } : null ;

            return result != null;
        }
    }
}
