using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;

    public sealed class ContentBaseRtspHeader
    {
        public static readonly string TypeName = "Content-Base";
        
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;

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
        
        public static bool TryParse( string input , out ContentBaseRtspHeader result )
        {
            result = null;

            var value = ValueAdapter.Adapt( input );

            if ( ! string.IsNullOrWhiteSpace( value ) )
            {
                result = new ContentBaseRtspHeader() { Uri = value };
            }

            return result != null;
        }
    }
}
