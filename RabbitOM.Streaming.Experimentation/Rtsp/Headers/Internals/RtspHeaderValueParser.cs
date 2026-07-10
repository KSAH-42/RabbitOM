using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal static class RtspHeaderValueParser
    {
        private const string QuotesChars = "\"'`";

        // TODO: change string separator parameter by a char parameter

        public static bool TryParse( string input , string separator , out KeyValuePair<string,string> result )
        {
            result = default;

            // TODO: refactor this code

            if ( TryParse( input , input?.Contains( separator ) == true ? separator : null , 2 , out string[] tokens ) )
            {
                result = new KeyValuePair<string, string>( tokens.ElementAtOrDefault( 0 ) , tokens.ElementAtOrDefault( 1 ) );
                return true;
            }

            return false;
        }

        // TODO: change string separator parameter by a char parameter

        public static bool TryParse( string input , string separator , out string[] result )
        {
            return TryParse( input , separator , null , out result );
        }

        // TODO: change string separator parameter by a char parameter

        public static bool TryParse( string input , string separator , int? maxTokens , out string[] result )
        {
            result = null;

            // TODO: try to remove string.IsNullOrWhiteSpace

            if ( string.IsNullOrWhiteSpace( input ) || string.IsNullOrEmpty( separator ) || separator.Any( element => QuotesChars.Contains( element ) ) )
            {
                return false;
            }

            // most of parser used string.split but here we do something different
            // because we need to ignore separators between quotes
            // and stop parse if we detect also none printable chars
            // like controls chars (DEL, tabs, CR, NLF), etc... 

            var segments = new List<string>();
            var builder = new StringBuilder();
            var insideQuotes = false;

            foreach ( var element in input )
            {
                if ( element <= 31 || element >= 127 )
                {
                    return false;
                }

                if ( QuotesChars.IndexOf( element ) >= 0 )
                {
                    insideQuotes = ! insideQuotes;
                }

                builder.Append( element );

                if ( ! insideQuotes )
                {
                    var segment = builder.ToString();

                    if ( segment.EndsWith( separator ) )
                    {
                        // TODO: adding units tests to test maxTokens parameter
                        if ( maxTokens.HasValue && maxTokens > 0 ) { -- maxTokens; }

                        if ( ! maxTokens.HasValue || maxTokens > 0 ) // do not use -- maxTokens > 0 to avoid recycle when it reach int.MinValue
                        {
                            segments.Add( segment.Substring( 0 , segment.Length - separator.Length ) );
                            builder.Clear();
                        }
                    }
                }
            }

            if ( builder.Length > 0 )
            {
                segments.Add( builder.ToString() );
            }

            // TODO: snipe this code: too slow

            var tokens = segments
                .Select( element => element.Trim() )
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