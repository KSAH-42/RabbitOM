using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core
{
    public static class RtspHeaderParser
    {
        private static readonly IReadOnlyCollection<char> QuotesChars = new HashSet<char>() { '\'' , '\"' , '`' };

        private static readonly StringRtspHeaderFilter ValueFilter = StringRtspHeaderFilter.UnQuoteFilter;

        private static readonly StringRtspHeaderValidator ValueValidator = StringRtspHeaderValidator.DefaultValidator;
        
        
        


        public static string Format( double value )
        {
            return value.ToString( "F3" , CultureInfo.InvariantCulture );
        }

        public static string Format( float value )
        {
            return value.ToString( "G2" , CultureInfo.InvariantCulture );
        }

        public static string Format( DateTime value )
        {
            value = value.Kind == DateTimeKind.Local ? value.ToUniversalTime() : value;

            return value.ToString( "r" , CultureInfo.InvariantCulture );
        }





        public static bool TryParse( string input , out int result )
        {
            return int.TryParse( ValueFilter.Filter( input ) , out result );
        }

        public static bool TryParse( string input , out long result )
        {
            return long.TryParse( ValueFilter.Filter( input ) , out result );
        }
        public static bool TryParse( string input , out byte result )
        {
            return byte.TryParse( ValueFilter.Filter( input ) , out result );
        }
        public static bool TryParse( string input , out double result )
        {
            return double.TryParse( ValueFilter.Filter( input ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out result );
        }

        public static bool TryParse( string input , out float result )
        {
            return float.TryParse( ValueFilter.Filter( input ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out result );
        }

        public static bool TryParse( string input , out DateTime result )
        {
            return DateTime.TryParse( ValueFilter.Filter( input ) , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out result );
        }
                





        public static bool TryParse( string input , string separator , out string[] result )
        {
            result = null;

            if ( ! ValueValidator.TryValidate( input ) )
            {
                return false;
            }

            if ( string.IsNullOrEmpty( separator ) )
            {
                result = new string[] { input };
                return true;
            }

            if ( separator.Any( element => QuotesChars.Contains( element ) ) )
            {
                return false;
            }
            

            var segments = new List<string>();
            var builder = new StringBuilder();
            var insideQuotes = false;

            foreach ( var element in input )
            {
                if ( QuotesChars.Contains( element ) )
                {
                    insideQuotes = ! insideQuotes;
                }
                else
                {
                    builder.Append( element );
                }

                if ( ! insideQuotes )
                {
                    var segment = builder.ToString();

                    if ( segment.EndsWith( separator ) )
                    {
                        segments.Add( segment.Substring( 0 , segment.Length - separator.Length ) );
                        builder.Clear();
                    }
                }
            }

            if ( builder.Length > 0 )
            {
                segments.Add( builder.ToString() );
            }

            var tokens = segments.Select( element => element.Trim() ).Where( element => ! string.IsNullOrWhiteSpace( element ) ).ToArray();

            if ( tokens.Length > 0 )
            {
                result = tokens;
            }

            return result != null;
        }
    }
}
