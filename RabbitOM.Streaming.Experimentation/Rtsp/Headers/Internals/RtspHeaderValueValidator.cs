using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal static class RtspHeaderValueValidator
    {
        private static string Symbols      = " /\\{}[]()\"'`!#$%&*+-.^_|~";
        
        private static string TokenSymbols = "!#$%&'*+-.^_|~";





        public static bool Contains( string value , Func<char , bool> predicate )
        {
            return ! string.IsNullOrEmpty( value ) && value.Any( predicate );
        }

        public static bool ContainsNoSpace( string value )
        {
            return ! string.IsNullOrEmpty( value ) && value.IndexOf( ' ' ) < 0;
        }





        
        public static string EnsureWellFormed( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value.All( character => char.IsLetterOrDigit( character ) || Symbols.IndexOf( character ) >= 0 ) ? value : throw new FormatException();
        }

        public static string EnsureWellFormedToken( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value.All( character => char.IsLetterOrDigit( character ) || TokenSymbols.IndexOf( character ) >= 0 ) ? value : throw new FormatException();
        }

        public static string EnsureWellFormedTokenOrEmpty( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return string.Empty;
            }

            return value.All( character => char.IsLetterOrDigit( character ) || TokenSymbols.IndexOf( character ) >= 0 ) ? value : throw new FormatException();
        }

        public static string EnsureContainsNoSpace( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value.Any( character => character == ' ' ) ? throw new FormatException() : value;
        }

        public static string EnsureHasLettersAndDigits( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value.Any( character => char.IsLetterOrDigit( character ) ) ? value : throw new FormatException();
        }

        public static string EnsureContains( string value , Func<char, bool> predicate )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value.Any( predicate ) ? value : throw new FormatException();
        }

        public static string EnsureNoQuotes( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value.All( character => character != '"' || character != '\'' || character != '`' ) ? value : throw new FormatException();
        }

        public static string EnsureNotNullOrEmpty( string value )
        {
            return ! string.IsNullOrEmpty( value ) ? value : throw new FormatException();
        }
        
        public static string EnsureNotNullOrWhiteSpace( string value )
        {
            return ! string.IsNullOrWhiteSpace( value ) ? value : throw new FormatException();
        }

        public static object EnsureNotNull( object value )
        {
            return value != null ? value : throw new ArgumentNullException( nameof( value ) );
        }

        public static TValue EnsureNotNull<TValue>( TValue value ) where TValue : class
        {
            return value != null ? value : throw new ArgumentNullException( nameof( value ) );
        }







        public static bool TryEnsureWellFormed( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return value.All( character => char.IsLetterOrDigit( character ) || Symbols.IndexOf( character ) >= 0 );
        }

        public static bool TryEnsureWellFormedToken( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return value.All( character => char.IsLetterOrDigit( character ) || TokenSymbols.IndexOf( character ) >= 0 );
        }

        public static bool TryEnsureWellFormedTokenOrEmpty( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return value.All( character => char.IsLetterOrDigit( character ) || TokenSymbols.IndexOf( character ) >= 0 );
        }


        public static bool TryEnsureNotNullOrEmpty( string value )
        {
            return ! string.IsNullOrEmpty( value );
        }

        public static bool TryEnsureNoSpace( string value )
        {
            return value?.IndexOf( ' ' ) >= 0;
        }
    }
}
