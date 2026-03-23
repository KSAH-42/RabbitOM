using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters
{
    public abstract class StringValueAdapter
    {
        public static StringValueAdapter TrimWithUnQuoteAdapter { get; } = new TrimWithUnQuoteValueAdapter();
        public static StringValueAdapter TrimWithSuppressQuoteAdapter { get; } = new TrimWithSuppressQuoteValueAdapter();
        

        public abstract string Adapt( string value );
    }
}
