using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters
{
    public sealed class QuoteValueAdapter : StringValueAdapter
    {
        public override string Adapt( string value )
        {
            return $"\"{ value?.Trim() ?? string.Empty }\"";
        }
    }
}
