using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    // TODO: find a way to reduce the number method

    internal static class RtspHeaderValueValidator
    {
        private static HashSet<char> Symbols      = " /\\{}[]()<>\"'`!#$%&*+-.^_|~".ToHashSet();

        private static HashSet<char> TokenSymbols = "!#$%&'*+-.^_|~".ToHashSet();






        public static string EnsureWellFormed( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value.All( character => char.IsLetterOrDigit( character ) || Symbols.Contains( character ) ) ? value : throw new FormatException();
        }

        public static string EnsureWellFormedOrEmpty( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return string.Empty;
            }

            return value.All( character => char.IsLetterOrDigit( character ) || Symbols.Contains( character ) ) ? value : throw new FormatException();
        }

        // TODO: need to accept empty string ?
        public static string EnsureWellFormedToken( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value.All( character => char.IsLetterOrDigit( character ) || Symbols.Contains( character ) ) ? value : throw new FormatException();
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

            return value.All( character => char.IsLetterOrDigit( character ) || TokenSymbols.Contains( character ) || predicate( character ) ) ? value : throw new FormatException();
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

            return value.All( character => (char.IsLetterOrDigit( character ) || TokenSymbols.Contains( character ) ) && predicate( character ) ) ? value : throw new FormatException();
        }

        public static string EnsureWellFormedTokenOrEmpty( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return string.Empty;
            }

            return value.All( character => char.IsLetterOrDigit( character ) || TokenSymbols.Contains( character ) ) ? value : throw new FormatException();
        }

        public static string EnsureLettersOrDigits( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value.Any( character => char.IsLetterOrDigit( character ) ) ? value : throw new FormatException();
        }

        public static string EnsureAny( string value , Func<char,int, bool> predicate )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                throw new ArgumentException( nameof( value ) );
            }

            return value.Where( predicate ).Any() ? value : throw new FormatException();
        }

        public static TValue EnsureNotNull<TValue>( TValue value ) where TValue : class
        {
            return value != null ? value : throw new ArgumentNullException( nameof( value ) );
        }







        public static bool IsWellFormed( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return value.All( character => char.IsLetterOrDigit( character ) || Symbols.Contains( character ) );
        }

        public static bool IsWellFormedOrEmpty( string value )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return true;
            }

            return value.All( character => char.IsLetterOrDigit( character ) || Symbols.Contains( character ) );
        }

        public static bool IsWellFormedToken( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return value.All( character => char.IsLetterOrDigit( character ) || TokenSymbols.Contains( character ) );
        }

        public static bool IsWellFormedTokenIfAll( string value , Func<char,bool> predicate )
        {
            if ( string.IsNullOrWhiteSpace( value ) || predicate == null )
            {
                return false;
            }

            return value.All( character => char.IsLetterOrDigit( character ) || TokenSymbols.Contains( character ) || predicate( character ) );
        }

        public static bool Any( string value , Func<char , bool> predicate )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return false;
            }

            return value.Any( predicate );
        }

        public static bool Any( string value , Func<char , int, bool> predicate )
        {
            if ( string.IsNullOrEmpty( value ) )
            {
                return false;
            }

            for ( var i = 0 ; i < value.Length ; ++ i )
            {
                if ( predicate( value[i] , i ) )
                {
                    return true;
                }
            }

            return false;
        }
    }
}
