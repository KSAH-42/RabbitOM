using System;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    public sealed class UriRtspHeaderValue
    {
        private Uri _uri;
        
        public Uri Value
        {
            get => _uri;
            set => _uri = RtspHeaderValueValidator.EnsureNotNull<Uri>( value );
        }

        public static bool TryParse( string input , out UriRtspHeaderValue result )
        {
            result = System.Uri.TryCreate( RtspHeaderValueSanitizer.UnQuotesWithTrim( input ) , UriKind.RelativeOrAbsolute , out var value ) 
                ? new UriRtspHeaderValue() { Value = value }
                : null;

            return result != null;
        }

        public override string ToString()
        {
            return _uri?.ToString() ?? string.Empty;
        }
    }
}
