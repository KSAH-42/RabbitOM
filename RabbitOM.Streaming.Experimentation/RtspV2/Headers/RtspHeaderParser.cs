using System;
using System.Diagnostics;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class RtspHeaderParser
    {
        public static bool TryParse( string input , string seperator , out string[] result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( input ) || string.IsNullOrEmpty( seperator ) )
            {
                Debug.Assert( ! string.IsNullOrEmpty( seperator ) );

                return false;
            }

            var tokens = StringRtspNormalizer.Normalize( input )
                .Split( new string[] { seperator } , StringSplitOptions.RemoveEmptyEntries )
                .Select( token => token.Trim() )
                .Where( token => ! string.IsNullOrWhiteSpace( token ) )
                .ToArray()
                ;

            if ( tokens.Length <= 0 )
            {
                return false;
            }

            result = tokens;

            return true;
        }
    }
}
