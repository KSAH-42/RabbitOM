using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core;

    public sealed class MediaDurationRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Media-Duration";

        public double Value { get; set; }

        public override string ToString()
        {
            return RtspHeaderParser.Format( Value );
        }

        public static bool TryParse( string input , out MediaDurationRtspHeader result )
        {
            result = RtspHeaderParser.TryParse( input , out double value ) ? new MediaDurationRtspHeader() { Value = value } : null;

            return result != null;
        }
    }
}
