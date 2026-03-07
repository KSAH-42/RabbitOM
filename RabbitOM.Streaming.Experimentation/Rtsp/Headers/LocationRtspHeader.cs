using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class LocationRtspHeader
    {
        public static readonly string TypeName = "Location";
        
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.UnQuoteAdapter;

        
        private string _uri = string.Empty;
        
        public string Uri
        {
            get => _uri;
            set => _uri = ValueAdapter.Adapt( value );
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace( Uri ) ? string.Empty : Uri;
        }

        public static bool TryParse( string input , out LocationRtspHeader result )
        {
            result = null;

            var value = ValueAdapter.Adapt( input );

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new LocationRtspHeader() { Uri = value };

            return true;
        }
    }
}
