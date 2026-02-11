using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class StringRtspValidator
    {
        private static readonly Regex AbsoluteUriRegex = new Regex( @"^[A-Za-z][A-Za-z0-9+\-\.]*:[^\s]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant );

        private static readonly Regex RelativeUriRegex = new Regex( @"^(?:\.\.?/|/|[A-Za-z0-9_\-\.~%]+(?:/[A-Za-z0-9_\-\.~%]*)*|[?].+|#.+)$", RegexOptions.Compiled | RegexOptions.CultureInvariant );

        private static readonly char[] ForbiddenChars = { ' ', '\t', '\r', '\n', '\f', '\v' };



        public static bool TryValidate( string value )
        {
            return ! string.IsNullOrWhiteSpace( value ) && ! value.Any( x => char.IsControl( x ) );
        }

        public static bool TryValidateUri( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var input = value.Trim();

            if ( input.IndexOfAny( ForbiddenChars ) >= 0 )
            {
                return false;
            }
            
            if ( AbsoluteUriRegex.IsMatch( input ) )
            {
                return Uri.TryCreate(input, UriKind.Absolute, out var uri ) && ! string.IsNullOrWhiteSpace( uri.Scheme );
            }

            if ( RelativeUriRegex.IsMatch( input ) )
            {
                return Uri.TryCreate(input, UriKind.Relative , out var uri ) && ! uri.IsAbsoluteUri;
            }

            return false;
        }
        
        public static bool TryValidateAsContentTD( string value )
        {
            return ! string.IsNullOrWhiteSpace( value )
                && value.Count( x => char.IsLetter( x ) ) > 0
                && value.Count( x => char.IsDigit( x ) ) >= 0
                ;
        }

        public static bool TryValidateAsContentSTD( string value )
        {
            // Star Text Digits
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            if ( value == "*" )
            {
                return true;
            }

            return value.Count( x => char.IsLetter( x ) ) > 0
                && value.Count( x => char.IsDigit( x ) ) >= 0
                ;
        }
    }
}
