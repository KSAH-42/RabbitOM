using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Filters;

    public sealed class ContentBaseRtspHeader
    {
        public static readonly string TypeName = "Content-Base";
        
        public static readonly StringRtspHeaderFilter ValueFilter = StringRtspHeaderFilter.UnQuoteFilter;

        private string _uri = string.Empty;

        public string Uri
        {
            get => _uri;
            set => _uri = ValueFilter.Filter( value );
        }

        
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace( Uri ) ? string.Empty : Uri;
        }
        
        public static bool TryParse( string input , out ContentBaseRtspHeader result )
        {
            result = null;

            var value = ValueFilter.Filter( input );

            if ( ! string.IsNullOrWhiteSpace( value ) )
            {
                result = new ContentBaseRtspHeader() { Uri = value };
            }

            return result != null;
        }
    }
}
