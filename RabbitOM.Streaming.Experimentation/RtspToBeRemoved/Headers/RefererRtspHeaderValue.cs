using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers
{
    using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers.Normalizers;
    
    public sealed class RefererRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Referer";
        
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;
        
        private string _uri = string.Empty;
        
        public string Uri
        {
            get => _uri;
            set => _uri = ValueNormalizer.Normalize( value );
        }

        public static bool TryParse( string input , out RefererRtspHeaderValue result )
        {
            result = null;

            var value = ValueNormalizer.Normalize( input );

            if ( RtspHeaderProtocolValidator.IsValidUri( value ) )
            {
                result = new RefererRtspHeaderValue() { Uri = value };
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace( Uri ) ? string.Empty : Uri ;
        }
    }
}
