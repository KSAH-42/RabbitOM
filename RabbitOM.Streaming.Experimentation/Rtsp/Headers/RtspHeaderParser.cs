using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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

        public static bool TryParse( string input , out Uri result )
        {
            result = null;

            if ( ! Uri.IsWellFormedUriString( input , UriKind.RelativeOrAbsolute ) )
            {
                return false;
            }

            return Uri.TryCreate( input , UriKind.RelativeOrAbsolute , out result );
        }

        public static bool TryParse( string input , string separator , out string[] result )
        {
            return TryParse( input , new string[] { separator } , out result );
        }

        public static bool TryParse( string input , string[] separators , out string[] result )
        {
            return TryParse( input , separators , SplitOptions.RemoveEmptyEntries | SplitOptions.TrimEntries , out result );
        }

        public static bool TryParse( string input , string[] seperators , SplitOptions options , out string[] result )
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

            System.Diagnostics.Debug.Assert( seperators.All( seperator => ! string.IsNullOrEmpty( seperator ) ) , "bad separator found" );
            
            var tokens = input.Split( seperators.Where( seperator => ! string.IsNullOrEmpty( seperator ) ).ToArray() , StringSplitOptions.None ) as IEnumerable<string>;
            
            if ( options.HasFlag( SplitOptions.TrimEntries ) )
            {
                tokens = tokens.Select( token => token.Trim() );
            }

            if ( options.HasFlag( SplitOptions.RemoveEmptyEntries ) )
            {
                tokens = tokens.Where( token => ! string.IsNullOrWhiteSpace( token ) );
            }

            if ( tokens.Count() > 0 )
            {
                result = tokens.ToArray();
            }

            return result != null;
        }
    }
}
