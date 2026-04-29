using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public static class Token
    {
        public static readonly IReadOnlyCollection<char> InvalidChars     = new HashSet<char>() { '²' , 'é' , '~' , 'ç' , 'è' , '$' , '£' , '€' , '¤' , '¨' , 'µ' , 'ù' , '^' , '§'  , '[' , ']' , '{' , '}' , '<' , '>' };
        public static readonly IReadOnlyCollection<char> QuotesChars      = new HashSet<char>() { '\'' , '\"' , '`'  };
        public static readonly IReadOnlyCollection<char> ParenthesisChars = new HashSet<char>() { '(' , ')' };


        public static bool IsValid( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return true; // accept empty value
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

        public static bool IsValid( string value , Func<char,bool> charValidator )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return true;
            }

            if ( charValidator == null )
            {
                throw new ArgumentNullException( nameof( charValidator ) );
            }

            foreach ( var element in value ?? string.Empty )
            {
                if ( ! IsValidChar( element ) || ! charValidator( element ) )
                {
                    return false;
                }
            }

            return true;
        }

        // TODO: rename this method or used enum
        public static bool IsValidToken( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return true; // accept empty value
            }

            var succeed = false;

            foreach ( var element in value )
            {
                if ( ! IsValidChar( element ) )
                {
                    return false;
                }

                succeed |= char.IsLetterOrDigit( element ) || element == '*' ;
            }

            return succeed;
        }

        private static bool IsValidChar( in char value )
        {
            return value <= 31 || value >= 127 || char.IsControl( value ) || InvalidChars.Contains( value ) ? false : true;
        }
    }
}
