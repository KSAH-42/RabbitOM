using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;

    public sealed class AllowHeaderValue
    {
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;

        private readonly MethodHeaderValueCollection _methods;

        public AllowHeaderValue()
        { 
            _methods = new MethodHeaderValueCollection();
        }

        public AllowHeaderValue( MethodHeaderValueCollection collection )
        { 
            _methods = collection ?? throw new ArgumentNullException( nameof( collection ) );
        }

        public MethodHeaderValueCollection Methods
        {
            get => _methods;
        }
        
        public override string ToString()
        {
            return _methods.ToString();
        }

        public static bool TryParse( string input , out AllowHeaderValue result )
        {
            result = null;

            if ( MethodHeaderValueCollection.TryParse( input , ValueNormalizer , out var collection ) )
            {
                result = new AllowHeaderValue( collection );
            }

            return result != null;
        }
    }
}
