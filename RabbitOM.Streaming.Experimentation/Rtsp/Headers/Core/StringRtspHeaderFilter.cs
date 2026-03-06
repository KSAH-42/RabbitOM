using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core
{
    public abstract class StringRtspHeaderFilter
    {
        public static StringRtspHeaderFilter TrimFilter { get; } = new TrimStringRtspHeaderFilter();
        public static StringRtspHeaderFilter QuoteFilter { get; } = new QuoteStringRtspHeaderFilter();
        public static StringRtspHeaderFilter UnQuoteFilter { get; } = new UnQuoteStringRtspHeaderFilter();


        public abstract string Filter( string value );
    }
}
