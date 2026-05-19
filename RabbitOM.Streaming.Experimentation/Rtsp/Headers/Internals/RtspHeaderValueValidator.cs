using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    // TODO: refactor and try to remove some methods in this class

    internal static class RtspHeaderValueValidator
    {
        private static string Symbols      = " /\\{}[]()<>\"'`!#$%&*+-.^_|~";
        
        private static string TokenSymbols = "!#$%&'*+-.^_|~";




        public static string EnsureWellFormed( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value.All( character => char.IsLetterOrDigit( character ) || Symbols.IndexOf( character ) >= 0 ) ? value : throw new FormatException();
        }

        public static string EnsureWellFormedOrEmpty( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return string.Empty;
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

        public static string EnsureWellFormedTokenIfAll( string value , Func<char,bool> predicate )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

            return value.All( character => char.IsLetterOrDigit( character ) || TokenSymbols.IndexOf( character ) >= 0 || predicate( character ) ) ? value : throw new FormatException();
        }

        public static string EnsureWellFormedTokenAndAll( string value , Func<char,bool> predicate )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

            return value.All( character => (char.IsLetterOrDigit( character ) || TokenSymbols.IndexOf( character ) >= 0 ) && predicate( character ) ) ? value : throw new FormatException();
        }

        public static string EnsureWellFormedTokenOrEmpty( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return string.Empty;
            }

            return value.All( character => char.IsLetterOrDigit( character ) || TokenSymbols.IndexOf( character ) >= 0 ) ? value : throw new FormatException();
        }

        public static string EnsureNoSpaces( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value.Any( character => character == ' ' ) ? throw new FormatException() : value;
        }

        public static string EnsureLettersOrDigits( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value.Any( character => char.IsLetterOrDigit( character ) ) ? value : throw new FormatException();
        }

        public static string EnsureAny( string value , Func<char, bool> predicate )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value.Any( predicate ) ? value : throw new FormatException();
        }

        public static string EnsureAny( string value , Func<char,int, bool> predicate )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value.Where( predicate ).Any() ? value : throw new FormatException();
        }

        public static string EnsureNotStartsWidth( string value , string text )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            if ( string.IsNullOrEmpty( text ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return ! value.StartsWith( text ) ? value : throw new FormatException();
        }

        public static string EnsureNotEndsWidth( string value , string text )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            if ( string.IsNullOrEmpty( text ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return ! value.EndsWith( text ) ? value : throw new FormatException();
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

        public static bool TryEnsureWellFormedOrEmpty( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return true;
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

        public static bool TryEnsureWellFormedTokenIfAll( string value , Func<char,bool> predicate )
        {
            if ( string.IsNullOrWhiteSpace( value ) || predicate == null )
            {
                return false;
            }

            return value.All( character => char.IsLetterOrDigit( character ) || TokenSymbols.IndexOf( character ) >= 0 || predicate( character ) );
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

        public static bool TryEnsureAny( string value , Func<char , bool> predicate )
        {
            return ! string.IsNullOrEmpty( value ) && value.Any( predicate );
        }

        public static bool TryEnsureAny( string value , Func<char , int, bool> predicate )
        {
            return ! string.IsNullOrEmpty( value ) && value.Where( predicate ).Any();
        }

        public static bool TryEnsureNotPrintable( in char value )
        {
            return value <= 31 || value >= 127;
        }

        public static bool TryEnsureLettersOrDigits( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return false;
            }

            return value.Any( character => char.IsLetterOrDigit( character ) );
        }

        public static bool TryEnsureNoSpaces( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return false;
            }

            return value.IndexOf( ' ' ) > 0;
        }

        public static bool TryEnsureNotStartsWidth( string value , string text )
        {
            if ( string.IsNullOrEmpty( value ) || string.IsNullOrEmpty( value ) )
            {
                return false;
            }

            return ! value.StartsWith( text );
        }

        public static bool TryEnsureNotEndsWidth( string value , string text )
        {
            if ( string.IsNullOrEmpty( value ) || string.IsNullOrEmpty( value ) )
            {
                return false;
            }

            return ! value.EndsWith( text );
        }
    }
}
