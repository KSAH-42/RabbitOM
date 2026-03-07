using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters
{
    public abstract class StringValueAdapter
    {
        public static StringValueAdapter TrimAdapter { get; } = new TrimValueAdapter();
        public static StringValueAdapter QuoteAdapter { get; } = new QuoteValueAdapter();
        public static StringValueAdapter UnQuoteAdapter { get; } = new UnQuoteValueAdapter();


        public abstract string Adapt( string value );
    }
}
