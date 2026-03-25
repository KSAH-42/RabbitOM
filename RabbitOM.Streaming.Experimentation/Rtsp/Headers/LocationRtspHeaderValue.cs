using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;
  
    public sealed class LocationRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Location";
        
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;
        
        private string _uri = string.Empty;
        
        public string Uri
        {
            get => _uri;
            set => _uri = ValueNormalizer.Normalize( value );
        }

        public static bool TryParse( string input , out LocationRtspHeaderValue result )
        {
            result = null;

            var value = ValueNormalizer.Normalize( input );

            if ( RtspHeaderProtocolValidator.IsValidUri( value ) )
            {
                result = new LocationRtspHeaderValue() { Uri = value };
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace( Uri ) ? string.Empty : Uri;
        }
    }
}
