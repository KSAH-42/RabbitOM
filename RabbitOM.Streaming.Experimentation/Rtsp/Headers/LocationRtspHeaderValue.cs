using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
  
    public sealed class LocationRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Location";
        
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;
        
        private string _uri = string.Empty;
        
        public string Uri
        {
            get => _uri;
            set => _uri = ValueAdapter.Adapt( value );
        }

        public static bool TryParse( string input , out LocationRtspHeaderValue result )
        {
            result = null;

            var value = ValueAdapter.Adapt( input );

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
