using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers
{
    public sealed class TrimWithUnQuoteValueNormalizer : StringValueNormalizer
    {
        private static readonly char[] SpaceAndQuotesChars = { ' ' , '\'' , '\"' , '`' };

        public override string Normalize( string value )
        {
            return value?.Trim( SpaceAndQuotesChars ) ?? string.Empty;
        }
    }
}
