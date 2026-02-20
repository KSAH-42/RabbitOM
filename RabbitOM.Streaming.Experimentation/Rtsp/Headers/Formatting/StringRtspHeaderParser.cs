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
            return TryParse( input , separators , SplitOptions.RemoveEmptyEntries | SplitOptions.TrimEntries , out result );
        }

        public static bool TryParse( string input , char[] seperators , SplitOptions options , out string[] result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return false;
            }

            if ( seperators == null || seperators.Length <= 0 )
            {
                return false;
            }

            var tokens = input.Split( seperators , StringSplitOptions.None );
            
            if ( options.HasFlag( SplitOptions.TrimEntries ) )
            {
                tokens = tokens.Select( token => token.Trim() ).ToArray();
            }

            if ( options.HasFlag( SplitOptions.RemoveEmptyEntries ) )
            {
                tokens = tokens.Where( token => ! string.IsNullOrWhiteSpace( token ) ).ToArray();
            }

            if ( tokens.Length > 0 )
            {
                result = tokens;
            }

            return result != null;
        }
    }
}
