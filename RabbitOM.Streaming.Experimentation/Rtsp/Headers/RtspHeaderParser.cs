using System;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class RtspHeaderParser
    {
        public static RtspHeaderFormatter Formatter { get; } = new RtspHeaderFormatter();

        /// use long.tryparse instead of int.tryparse for fallback
        public static bool TryParse( string input , out int result )
        {
            return int.TryParse( Formatter.Filter( input ) , out result );
        }

        public static bool TryParse( string input , out long result )
        {
            return long.TryParse( Formatter.Filter( input ) , out result );
        }

        public static bool TryParse( string input , out float result )
        {
            return float.TryParse( Formatter.Filter( input ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out result );
        }

        public static bool TryParse( string input , out double result )
        {
            return double.TryParse( Formatter.Filter( input ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out result );
        }

        public static bool TryParse( string input , out DateTime result )
        {
            return DateTime.TryParse( input , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out result );
        }

        // don't unquote before returning result the quote could be the seperator
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
