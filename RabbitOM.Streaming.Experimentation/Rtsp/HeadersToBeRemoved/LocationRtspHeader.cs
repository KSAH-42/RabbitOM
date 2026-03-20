using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved
{
    using RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved.Adapters;
  
    public sealed class LocationRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Location";
        
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;
        
        private string _uri = string.Empty;
        
        public string Uri
        {
            get => _uri;
            set => _uri = ValueAdapter.Adapt( value );
        }

        public static bool TryParse( string input , out LocationRtspHeader result )
        {
            result = null;

            var value = ValueAdapter.Adapt( input );

            if ( RtspHeaderProtocolValidator.TryValidateUri( value ) )
            {
                result = new LocationRtspHeader() { Uri = value };
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace( Uri ) ? string.Empty : Uri;
        }
    }
}
