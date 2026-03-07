using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters
{
    public sealed class TrimWithUnQuoteValueAdapter : StringValueAdapter
    {
        private static readonly char[] SpaceAndQuotesChars = { ' ' , '\'' , '\"' , '`' };

        public override string Adapt( string value )
        {
            return value?.Trim( SpaceAndQuotesChars ) ?? string.Empty;
        }
    }
}
