using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved.Adapters
{
    public sealed class TrimValueAdapter : StringValueAdapter
    {
        public override string Adapt( string value )
        {
            return value?.Trim() ?? string.Empty;
        }
    }
}
