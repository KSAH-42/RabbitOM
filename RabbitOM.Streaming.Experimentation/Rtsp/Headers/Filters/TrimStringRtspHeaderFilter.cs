using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Filters
{
    public sealed class TrimStringRtspHeaderFilter : StringRtspHeaderFilter
    {
        public override string Filter( string value )
        {
            return value?.Trim()?? string.Empty;
        }
    }
}
