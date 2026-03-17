using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
  
    public sealed class UriRtspHeaderValue
    {
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;
        
        private Uri _uri;
        
        public Uri Uri
        {
            get => _uri;
            set => _uri = value;
        }

        public static bool TryParse( string input , out UriRtspHeaderValue result )
        {
            result = null;

            var value = ValueAdapter.Adapt( input );

            if ( RtspHeaderValueValidator.TryValidateUri( value ) && Uri.TryCreate( value , UriKind.RelativeOrAbsolute , out var uri ) )
            {
                result = new UriRtspHeaderValue() { Uri = uri };
            }

            return result != null;
        }

        public override string ToString()
        {
            return _uri?.ToString() ?? string.Empty;
        }
    }
}
