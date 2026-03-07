using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Filters
{
    public sealed class UnQuoteStringRtspHeaderFilter : StringRtspHeaderFilter
    {
        private static readonly char[] SpaceAndQuotesChars = { ' ' , '\'' , '\"' , '`' };

        public override string Filter( string value )
        {
            return value?.Trim( SpaceAndQuotesChars ) ?? string.Empty;
        }
    }
}
