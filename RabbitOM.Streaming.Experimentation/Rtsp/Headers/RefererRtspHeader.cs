using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core;

    public sealed class RefererRtspHeader : RtspHeader
    {
        public static readonly string TypeName = "Referer";
        
        public static readonly StringRtspHeaderFilter ValueFilter = StringRtspHeaderFilter.UnQuoteFilter;
        
        private string _uri = string.Empty;
        
        public string Uri
        {
            get => _uri;
            set => _uri = ValueFilter.Filter( value );
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace( Uri ) ? string.Empty : Uri ;
        }

        public static bool TryParse( string input , out RefererRtspHeader result )
        {
            result = null;

            var value = ValueFilter.Filter( input );

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            result = new RefererRtspHeader() { Uri = value };

            return true;
        }
    }
}
