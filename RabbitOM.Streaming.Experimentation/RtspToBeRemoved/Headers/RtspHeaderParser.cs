using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers
{
    internal static class RtspHeaderParser
    {
        private static readonly IReadOnlyCollection<char> QuotesChars = new HashSet<char>() { '\'' , '\"' , '`' };      
        
        public static bool TryParse( string input , string separator , out KeyValuePair<string,string> result )
        {
            result = default;

            if ( TryParse( input , input?.Contains( separator ) == true ? separator : null , 2 , out string[] tokens ) )
            {
                result = new KeyValuePair<string, string>( tokens.ElementAtOrDefault( 0 ) , tokens.ElementAtOrDefault( 1 ) );
                return true;
            }

            return false;
        }
        
        public static bool TryParse( string input , string separator , out string[] result )
        {
            return TryParse( input , separator , null , out result );
        }

        public static bool TryParse( string input , string separator , int? maxTokens , out string[] result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( input ) || string.IsNullOrEmpty( separator ) || separator.Any( element => QuotesChars.Contains( element ) ) )
            {
                return false;
            }
            
            var segments = new List<string>();
            var builder = new StringBuilder();
            var insideQuotes = false;

            // we don't used string.split here
            // because we need to ignore separators between quotes

            foreach ( var element in input )
            {
                if ( QuotesChars.Contains( element ) )
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
                        maxTokens = maxTokens.HasValue && maxTokens > 0 ? -- maxTokens : maxTokens;

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

            var tokens = segments
                .Select( element => element.Trim() )
                .Where( element => ! string.IsNullOrWhiteSpace( element ) )
                .ToArray();

            if ( tokens.Length > 0 )
            {
                result = tokens;
            }

            return result != null;
        }
    }
}
