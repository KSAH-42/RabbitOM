using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers
{
    using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers.Normalizers;
    
    public sealed class ContentBaseRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Content-Base";
        
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;
       
        private string _uri = string.Empty;

        public string Uri
        {
            get => _uri;
            set => _uri = ValueNormalizer.Normalize( value );
        }

        public static bool TryParse( string input , out ContentBaseRtspHeaderValue result )
        {
            result = null;

            var value = ValueNormalizer.Normalize( input );

            if ( RtspHeaderProtocolValidator.IsValidUri( value ) )
            {
                result = new ContentBaseRtspHeaderValue() { Uri = value };
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace( Uri ) ? string.Empty : Uri;
        }
        
    }
}
