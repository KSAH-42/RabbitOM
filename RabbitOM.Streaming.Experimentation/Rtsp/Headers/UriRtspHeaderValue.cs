using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class UriRtspHeaderValue
    {
        private string _uri = string.Empty;
        
        public string Uri
        {
            get => _uri;
            set => _uri = RtspHeaderValueSanitizer.UnQuotesWithTrim( value );
        }

        public static bool TryParse( string input , out UriRtspHeaderValue result )
        {
            result = null;

            var value = RtspHeaderValueSanitizer.UnQuotesWithTrim( input );

            if ( string.IsNullOrWhiteSpace( value ) || ! System.Uri.IsWellFormedUriString( value , UriKind.RelativeOrAbsolute ) )
            {
                return false;
            }

            result = new UriRtspHeaderValue() { Uri = value };
            
            return true;
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace( Uri ) ? string.Empty : Uri;
        }
    }
}
