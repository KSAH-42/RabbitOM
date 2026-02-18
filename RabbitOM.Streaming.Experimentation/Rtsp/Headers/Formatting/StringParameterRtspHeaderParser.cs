using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting
{
    public static class StringParameterRtspHeaderParser
    {
        public static bool TryParse( string input , char separator , out StringParameter result )
        {
            return TryParse( input , new char[] { separator } , out result );
        }

        public static bool TryParse( string input , char[] separators , out StringParameter result )
        {
            result = default;

            if ( string.IsNullOrWhiteSpace( input ) || input.IndexOfAny( separators ) < 0 )
            {
                return false;
            }

            var tokens = input
                .Split( separators , StringSplitOptions.RemoveEmptyEntries )
                .Select( token => token.Trim() )
                .Where( token => ! string.IsNullOrEmpty( token ) )
                .ToArray()
                ;

            if ( tokens.Length <= 0 )
            {
                return false;
            }

            result = new StringParameter( tokens.ElementAtOrDefault( 0 ) , tokens.ElementAtOrDefault( 1 ) );

            return true;
        }
    }
}
