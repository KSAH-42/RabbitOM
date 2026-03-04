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

            if ( string.IsNullOrWhiteSpace( input ) )
            {
                return false;
            }

            if ( string.IsNullOrEmpty( seperator ) )
            {
                result = new string[] { input };
                return true;
            }

            if ( seperator.IndexOfAny( QuotesChars ) >= 0 )
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

                    if ( segment.EndsWith( seperator ) )
                    {
                        segments.Add( segment.Substring( 0 , segment.Length - seperator.Length ) );
                        builder.Clear();
                    }
                }
            }

            if ( builder.Length > 0 )
            {
                segments.Add( builder.ToString() );
            }

            var tokens = segments
                .Select( element => element.Trim(  ) )
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
