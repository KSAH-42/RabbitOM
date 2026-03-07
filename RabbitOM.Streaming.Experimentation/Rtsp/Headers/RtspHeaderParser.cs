using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal static class RtspHeaderParser
    {
        private static readonly IReadOnlyCollection<char> QuotesChars = new HashSet<char>() { '\'' , '\"' , '`' };

        
        
        
        public static bool TryParse( string input , string separator , out string[] result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( input ) || string.IsNullOrEmpty( separator ) || separator.Any( element => QuotesChars.Contains( element ) ) )
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
