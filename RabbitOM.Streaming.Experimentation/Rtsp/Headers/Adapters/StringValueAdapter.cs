using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters
{
    public abstract class StringValueAdapter
    {
        public static StringValueAdapter TrimAdapter { get; } = new TrimValueAdapter();
        public static StringValueAdapter TrimWithUnQuoteAdapter { get; } = new TrimWithUnQuoteValueAdapter();
        public static StringValueAdapter TrimWithSuppressQuoteAdapter { get; } = new TrimWithSuppressQuoteValueAdapter();
        public static StringValueAdapter QuoteAdapter { get; } = new QuoteValueAdapter();
        public static StringValueAdapter UnQuoteAdapter { get; } = new UnQuoteValueAdapter();


        public abstract string Adapt( string value );
    }
}
