using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal static class RtspHeaderParser
    {
        private static readonly char[] QuotesChars = { '\'' , '\"' };
       
        private static readonly char[] TrimedChars = QuotesChars.Append( ' ' ).ToArray();






        public static RtspHeaderFormatter Formatter { get; } = new RtspHeaderFormatter();

        public static RtspHeaderTokenValidator TokenValidator { get; } = new RtspHeaderTokenValidator();






        public static bool TryParse( string input , out float result )
        {
            return float.TryParse( input?.Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out result );
        }

        public static bool TryParse( string input , out double result )
        {
            return double.TryParse( input?.Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out result );
        }

        public static bool TryParse( string input , out DateTime result )
        {
            return DateTime.TryParse( input , CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal , out result );
        }

        public static bool TryParse( string input , string seperator , out string[] result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( input ) || string.IsNullOrEmpty( seperator ) )
            {
                return false;
            }

            if ( seperator.IndexOfAny( QuotesChars ) >= 0 )
            {
                throw new InvalidOperationException( "quotes are absolutely forbidden used as seperator: fix your code" );
            }

            var segments = new List<string>();
            var builder = new StringBuilder();
            var window = new StringBuilder();
            var quoteFound = false;

            foreach ( var element in input )
            {
                window.Append( element );

                if ( window.Length >= seperator.Length )
                {
                    window.Remove( 0 , 1 );
                }

                if ( QuotesChars.Contains( element ) )
                {
                    quoteFound = ! quoteFound;
                }
            
                builder.Append( element );

                if ( ! quoteFound && builder.ToString().EndsWith( seperator ) )
                {
                    segments.Add( builder.Remove( builder.Length - 1 - window.Length , window.Length + 1 ).ToString() );
                    builder.Clear();
                }
            }

            if ( builder.Length > 0 )
            {
                segments.Add( builder.ToString() );
            }

            var tokens = segments
                .Select( element => element.Trim( TrimedChars ) )
                .Where( element => ! string.IsNullOrWhiteSpace( element ) )
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
