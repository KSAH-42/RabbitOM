using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers
{
    public static class StringRtspHeaderParser
    {
        public static readonly char AcceptAllChar = '*';

        public static readonly char QuoteChar = '\"';

        public static readonly char[] QuotesChars = { '\'' , '\"' , '`' };

        public static readonly char[] SpaceWithQuotesChars = { ' ' , '\'' , '\"' , '`' };

        public static readonly char[] SpaceWithCommaAndSemiColon = { ' ', ',', ';', };

        public static readonly char[] InvalidChars = { '²' , 'é' , '~' , 'ç' , 'è' , '$' , '£' , '€' , '¤' , '¨' , 'µ' , 'ù' , '^' , '§' , '\r' , '\n' , '\t' , '\f' , '\v'  };

        public static readonly char[] ParenthesisChars = { '(' , ')' };

        

        public static string TrimValue( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return string.Empty;
            }

            if ( IsInvalid( value ) )
            {
                throw new FormatException("the input contains invalid characters");
            }

            return value.Trim();
        }

        public static string TrimValue( string value , char trimedChar )
        {
            return TrimValue( value , new char[] { trimedChar } );
        }

        public static string TrimValue( string value , char[] trimedChars )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return string.Empty;
            }

            if ( IsInvalid( value ) )
            {
                throw new FormatException("the input contains invalid characters");
            }

            return value.Trim( trimedChars ?? Array.Empty<char>() );
        }

        public static string Quote( string value )
        {
            return $"{QuoteChar}{value??string.Empty}{QuoteChar}";
        }

        public static string UnQuote( string value )
        {
            return value?.Trim( QuotesChars ) ?? string.Empty;
        }

        public static bool ContainsAnyLettersOrDigits( string value )
        {
            return value?.Any( element => char.IsLetterOrDigit( element ) ) ?? false;
        }

        public static bool IsInvalid( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return false;
            }

            if ( value.Any( element => element <= 31 || element >= 127 || char.IsControl( element ) ) )
            {
                return true;
            }

            var illegalChars = new HashSet<char>( InvalidChars );

            return value.Any( element => illegalChars.Contains( element ) );
        }

        // add a test for: the tryparse method when it musts returns true it should always with a result greater than zero and contains an none empty value
    
        public static bool TryParse( string input , string separator , out string[] result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( input ) || IsInvalid( input ) )
            {
                return false;
            }

            if ( IsInvalid( separator ) || separator?.IndexOfAny( QuotesChars ) >= 0 )
            {
                return false;
            }

            if ( string.IsNullOrEmpty( separator ) )
            {
                result = new string[] { input };
                return true;
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
