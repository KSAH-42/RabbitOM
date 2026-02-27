using System;
using System.Linq;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal static class RtspHeaderParser
    {
        public static string Format( DateTime value )
        {
            value = value.Kind == DateTimeKind.Local ? value.ToUniversalTime() : value;

            return value.ToString( "r" , CultureInfo.InvariantCulture );
        }

        public static bool TryParse( string input , out DateTime result )
        {
            return DateTime.TryParse( input , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out result );
        }

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
