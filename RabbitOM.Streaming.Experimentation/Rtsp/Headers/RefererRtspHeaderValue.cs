using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
    
    public sealed class RefererRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Referer";
        
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;
        
        private string _uri = string.Empty;
        
        public string Uri
        {
            get => _uri;
            set => _uri = ValueAdapter.Adapt( value );
        }

        public static bool TryParse( string input , out RefererRtspHeaderValue result )
        {
            result = null;

            var value = ValueAdapter.Adapt( input );

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
