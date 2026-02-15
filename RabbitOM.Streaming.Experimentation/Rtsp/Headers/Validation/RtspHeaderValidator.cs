using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation
{
    public static class RtspHeaderValidator
    {
        private static readonly Regex AbsoluteUriExpression = new Regex( @"^[A-Za-z][A-Za-z0-9+\-\.]*:[^\s]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant );

        private static readonly Regex RelativeUriExpression = new Regex( @"^(?:\.\.?/|/|[A-Za-z0-9_\-\.~%]+(?:/[A-Za-z0-9_\-\.~%]*)*|[?].+|#.+)$", RegexOptions.Compiled | RegexOptions.CultureInvariant );
        
        private static readonly IReadOnlyCollection<char> ForbiddenTokenChars = new HashSet<char>() { ' ' , '(' , ')' , '[' , ']' , '{' ,  '}' , '<' , '>' , ',' , ';' , ':' , '=' , '?' , '¨' , '¤' , 'é' , 'è' , 'à' , 'ù' , 'ç' };
        
        private static readonly IReadOnlyCollection<char> ForbiddenUriChars = new HashSet<char>() { ' ' , '(' , ')' , '[' , ']' , '{' ,  '}' , '<' , '>' , 'é' , 'è' , 'à' , 'ù' , 'ç' };








        public static bool TryValidate( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            if ( value.Any( character => character <= 31 || character >= 127  ) )
            {
                return false;
            }

            return true;
        }

        public static bool TryValidateAsToken( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            if ( value.Any( character => character <= 31 || character >= 127 || ForbiddenTokenChars.Contains( character ) ) )
            {
                return false;
            }

            return true;
        }

        public static bool TryValidateAsUri( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var input = value.Trim();

            if ( value.Any( character => character <= 31 || character >= 127 || ForbiddenUriChars.Contains( character ) ) )
            {
                return false;
            }

            if ( AbsoluteUriExpression.IsMatch( input ) )
            {
                return Uri.TryCreate(input, UriKind.Absolute, out var uri ) && ! string.IsNullOrWhiteSpace( uri.Scheme );
            }

            if ( RelativeUriExpression.IsMatch( input ) )
            {
                return Uri.TryCreate(input, UriKind.Relative , out var uri ) && ! uri.IsAbsoluteUri;
            }
            
            return false;
        }

        public static bool TryValidateAsLong( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            if ( value.Any( character => character <= 31 || character >= 127  ) )
            {
                return false;
            }

            value = value.Replace( "\"" , "" ).Replace( "'"  , "" );

            return long.TryParse( value , out var number );
        }
    }
}
