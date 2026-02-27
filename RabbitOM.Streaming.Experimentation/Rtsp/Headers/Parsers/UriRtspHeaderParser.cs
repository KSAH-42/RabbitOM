using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal static class UriRtspHeaderParser
    {
        public static bool TryParse( string input , out Uri result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return false;
            }

            if ( ! Uri.IsWellFormedUriString( input , UriKind.RelativeOrAbsolute ) )
            {
                return false;
            }

            return Uri.TryCreate( input , UriKind.RelativeOrAbsolute , out result );
        }
    }
}
