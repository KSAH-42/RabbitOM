using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances
{
    public abstract class StringValueNormalizer : INormalizer<string>
    {
        public static StringValueNormalizer TrimWithUnQuoteNormalizer { get; } = new TrimWithUnQuoteValueNormalizer();
       
        public static StringValueNormalizer TrimWithRemoveAllQuotesNormalizer { get; } = new TrimWithRemoveAllQuotesValueNormalizer();
        



        public abstract string Normalize( string value );
    }
}
