using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation;

    public sealed class RefererRtspHeader
    {
        public static readonly string TypeName = "Referer";
        
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;
        public static readonly StringValueValidator UriValidator = StringValueValidator.UriValidator;

        private string _uri = string.Empty;
        
        public string Uri
        {
            get => _uri;
            set => _uri = ValueAdapter.Adapt( value );
        }

        public static bool TryParse( string input , out RefererRtspHeader result )
        {
            result = null;

            var value = ValueAdapter.Adapt( input );

            if ( UriValidator.TryValidate( value ) )
            {
                result = new RefererRtspHeader() { Uri = value };
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace( Uri ) ? string.Empty : Uri ;
        }
    }
}
