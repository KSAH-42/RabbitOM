using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting
{
    public static class StringRtspHeaderParser
    {
        public static bool TryParse( string input , char separator , out string[] result )
        {
            return TryParse( input , new char[] { separator } , out result );
        }

        public static bool TryParse( string input , char[] separators , out string[] result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return false;
            }

            if ( input.IndexOfAny( separators ) < 0 )
            {
                return false;
            }

            var tokens = input
                .Split( separators , StringSplitOptions.RemoveEmptyEntries )
                .Select( token => token.Trim() )
                .Where( token => ! string.IsNullOrEmpty( token ) )
                .ToArray()
                ;

            if ( tokens.Length > 0 )
            {
                result = tokens;
            }

            return result != null;
        }
    }
}
