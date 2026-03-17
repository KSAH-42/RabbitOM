using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved.Adapters
{
    public abstract class StringValueAdapter
    {
        public static StringValueAdapter TrimAdapter { get; } = new TrimValueAdapter();
        public static StringValueAdapter TrimWithUnQuoteAdapter { get; } = new TrimWithUnQuoteValueAdapter();
        public static StringValueAdapter QuoteAdapter { get; } = new QuoteValueAdapter();


        public abstract string Adapt( string value );
    }
}
