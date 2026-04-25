using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;

    public sealed class AcceptHeaderValue
    {
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;

        private readonly StringWithQualityHeaderValueCollection _mimes;

        public AcceptHeaderValue()
        { 
            _mimes = new StringWithQualityHeaderValueCollection();
        }

        public AcceptHeaderValue( StringWithQualityHeaderValueCollection collection )
        { 
            _mimes = collection ?? throw new ArgumentNullException( nameof( collection ) );
        }

        public StringWithQualityHeaderValueCollection Methods
        {
            get => _mimes;
        }
        
        public override string ToString()
        {
            return _mimes.ToString();
        }

        public static bool TryParse( string input , out AcceptHeaderValue result )
        {
            result = null;

            if ( StringWithQualityHeaderValueCollection.TryParse( input , ValueNormalizer , out var collection ) )
            {
                result = new AcceptHeaderValue( collection );
            }

            return result != null;
        }
    }
}
