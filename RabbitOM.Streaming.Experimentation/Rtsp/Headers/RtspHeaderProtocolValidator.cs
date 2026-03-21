using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    public static class RtspHeaderProtocolValidator
    {
        private static readonly IReadOnlyCollection<char> InvalidChars     = new HashSet<char>() { '²' , 'é' , '~' , 'ç' , 'è' , '$' , '£' , '€' , '¤' , '¨' , 'µ' , 'ù' , '^' , '§'  , '[' , ']' , '{' , '}' , '<' , '>' };
        private static readonly IReadOnlyCollection<char> QuotesChars      = new HashSet<char>() { '\'' , '\"' , '`'  };
        private static readonly IReadOnlyCollection<char> ParenthesisChars = new HashSet<char>() { '(' , ')' };


        public static Func<string,bool> NullStringValiator { get; } = _ => true;


        public static bool IsValidChar( in char value )
        {
            return value <= 31 || value >= 127 || char.IsControl( value ) || InvalidChars.Contains( value ) ? false : true;
        }
        
        public static bool IsValidVersion( string value )
        {
            return Version.TryParse( value , out _ );
        }

        public static bool IsValidMime( string value )
        {
            return SupportedTypes.Mimes.Contains( value );
        }

        public static bool IsValidLanguage( string value )
        {
            return SupportedTypes.Languages.Contains( value );
        }

        public static bool IsValidEncoding( string value )
        {
            return SupportedTypes.Encodings.Contains( value );
        }

        public static bool IsValidUri( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return Uri.IsWellFormedUriString( value , UriKind.RelativeOrAbsolute );
        }

        public static bool IsValid( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }
            
            foreach ( var element in value )
            {
                if ( ! IsValidChar( element ) )
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsValidToken( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var succeed = false;

            foreach ( var element in value )
            {
                if ( ! IsValidChar( element ) || QuotesChars.Contains( element ) )
                {
                    return false;
                }

                succeed |= char.IsLetterOrDigit( element ) || element == '*' ;
            }

            return succeed;
        }

        public static bool IsWellFormedComment( string value )
        {
            foreach ( var element in value ?? string.Empty )
            {
                if ( ! IsValidChar( element ) || QuotesChars.Contains( element ) || ParenthesisChars.Contains( element ) )
                {
                    return false;
                }
            }

            return true;
        }        
    }
}
