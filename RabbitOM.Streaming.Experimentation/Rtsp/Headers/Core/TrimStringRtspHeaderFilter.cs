using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core
{
    public sealed class TrimStringRtspHeaderFilter : StringRtspHeaderFilter
    {
        public override string Filter( string value )
        {
            return value?.Trim()?? string.Empty;
        }
    }
}
