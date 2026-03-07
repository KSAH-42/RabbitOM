using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation;

    public sealed class ContentBaseRtspHeader
    {
        public static readonly string TypeName = "Content-Base";
        
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;
        public static readonly StringValueValidator UriValidator = StringValueValidator.UriValidator;

        private string _uri = string.Empty;

        public string Uri
        {
            get => _uri;
            set => _uri = ValueAdapter.Adapt( value );
        }

        public static bool TryParse( string input , out ContentBaseRtspHeader result )
        {
            result = null;

            var value = ValueAdapter.Adapt( input );

            if ( UriValidator.TryValidate( value ) )
            {
                result = new ContentBaseRtspHeader() { Uri = value };
            }

            return result != null;
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace( Uri ) ? string.Empty : Uri;
        }
        
    }
}
