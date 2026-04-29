using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Compliances
{
    public abstract class StringValueNormalizer : INormalizer<string>
    {
        public static StringValueNormalizer TrimWithUnQuoteNormalizer { get; } = new TrimWithUnQuoteValueNormalizer();
       
        public static StringValueNormalizer TrimWithSuppressQuoteNormalizer { get; } = new TrimWithQuoteSuppressValueNormalizer();
        



        public abstract string Normalize( string value );
    }
}
