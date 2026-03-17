using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved
{
    public static class RtspHeaderValueValidator
    {
        private static readonly IReadOnlyCollection<char> InvalidChars = new HashSet<char>() { '²' , 'é' , '~' , 'ç' , 'è' , '$' , '£' , '€' , '¤' , '¨' , 'µ' , 'ù' , '^' , '§'  , '[' , ']' , '{' , '}' , '<' , '>' };
        
        private static readonly IReadOnlyCollection<char> QuotesChars = new HashSet<char>() { '\'' , '\"' , '`'  };

        private static readonly IReadOnlyCollection<char> ParenthesisChars = new HashSet<char>() { '(' , ')' };



        
        public static bool TryValidate( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }
            
            foreach ( var element in value )
            {
                if ( element <= 31 || element >= 127 || char.IsControl( element ) )
                {
                    return false;
                }

                if ( InvalidChars.Contains( element ) )
                {
                    return false;
                }
            }

            return true;
        }

        public static bool TryValidateToken( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var succeed = false;

            foreach ( var element in value )
            {
                if ( element <= 31 || element >= 127 || char.IsControl( element ) )
                {
                    return false;
                }

                if ( InvalidChars.Contains( element ) || QuotesChars.Contains( element ) )
                {
                    return false;
                }

                succeed |= char.IsLetterOrDigit( element ) || element == '*' ;
            }

            return succeed;
        }

        public static bool TryValidateComment( string value )
        {
            foreach ( var element in value ?? string.Empty )
            {
                if ( element <= 31 || element >= 127 || char.IsControl( element ) )
                {
                    return false;
                }

                if ( InvalidChars.Contains( element ) || QuotesChars.Contains( element ) || ParenthesisChars.Contains( element ) )
                {
                    return false;
                }
            }

            return true;
        }

        public static bool TryValidateUri( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return Uri.IsWellFormedUriString( value , UriKind.RelativeOrAbsolute );
        }

        public static bool TryValidateVersion( string value )
        {
            return Version.TryParse( value , out _ );
        }
    }
}
