using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers.Normalizers
{
    public abstract class StringValueNormalizer
    {
        public static StringValueNormalizer TrimWithUnQuoteNormalizer { get; } = new TrimWithUnQuoteValueNormalizer();
        public static StringValueNormalizer TrimWithSuppressQuoteNormalizer { get; } = new TrimWithQuoteSuppressValueNormalizer();
        

        public abstract string Normalize( string value );
    }
}
