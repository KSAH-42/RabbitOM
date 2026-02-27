using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal static class StringRtspHeaderParser
    {
        public static bool TryParse( string input , string separator , out string[] result )
        {
            result = null;

            System.Diagnostics.Debug.Assert( ! string.IsNullOrEmpty( separator ) , "bad separator" );

            if ( string.IsNullOrWhiteSpace( input ) || string.IsNullOrEmpty( separator ) )
            {
                return false;
            }

            var tokens = input.Split( new string[] { separator } , StringSplitOptions.None )
                .Select( token => token.Trim() )
                .Where( token => ! string.IsNullOrWhiteSpace( token ) )
                .ToArray()
                ;

            if ( tokens.Count() > 0 )
            {
                result = tokens.ToArray();
            }

            return result != null;
        }
    }
}
