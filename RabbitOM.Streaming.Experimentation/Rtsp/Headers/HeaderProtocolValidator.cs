using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types;

    internal static class HeaderProtocolValidator
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

        public static bool IsValidTransport( string value )
        {
            return TransportTypes.Values.Count == 0 || TransportTypes.Values.Contains( value );
        }

        public static bool IsValidTransmission( string value )
        {
            return TransmissionTypes.Values.Count == 0 || TransmissionTypes.Values.Contains( value );
        }

        public static bool IsValidLanguage( string value )
        {
            return LanguageTypes.Values.Count == 0 || LanguageTypes.Values.Contains( value );
        }

        public static bool IsValidEncoding( string value )
        {
            return EncodingTypes.Values.Count == 0 || EncodingTypes.Values.Contains( value );
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

        public static bool IsValidMime( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var succeed = false;

            foreach ( var element in value )
            {
                if ( ! IsValidChar( element ) || QuotesChars.Contains( element ) || element == ' ' )
                {
                    return false;
                }

                succeed |= char.IsLetterOrDigit( element ) || element == '*' ;
            }

            return succeed;
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
                if ( ! IsValidChar( element ) )
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

        public static bool IsValidWarningAgent( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }
            
            foreach ( var element in value )
            {
                if ( ! IsValidChar( element ) || element == ' ' )
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsValidWarningCode( int code )
        {
            return code >= 0;
        }
    }
}
