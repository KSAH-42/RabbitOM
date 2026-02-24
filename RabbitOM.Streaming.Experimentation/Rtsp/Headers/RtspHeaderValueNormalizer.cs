using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class RtspHeaderValueNormalizer
    {
        private static readonly IReadOnlyCollection<char> ForbiddenCharacters = new HashSet<char>
        { 
            '$' , '£' , '€' ,
            'é' , 'è' , 'à' , 'ù' , 'ç' , 'µ' , 'ù' , '²' ,
            '¨' , '^' , '§' , '¤' , 
        };

        private static readonly char[] TrimCharacters =
        {
            ' ',
            ',',
            ';',
        };








        
        public static bool CheckValue( char value )
        {
            return value > 31 && value < 127 && ! ForbiddenCharacters.Contains( value );
        }

        public static bool CheckValue( string value )
        {
            return value?.All( CheckValue ) ?? false;
        }

        public static string Normalize( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return string.Empty;
            }

            return new string( value.Where( character => CheckValue( character ) ).ToArray() )
                .Replace( "'" , "" )
                .Replace( "\"" , "" )
                .Trim( TrimCharacters )
                ;
        }

        public static string Normalize( string value , params string[] extraFilters )
        {
            if ( extraFilters == null )
            {
                throw new ArgumentNullException( nameof( extraFilters) );
            }

            if ( extraFilters.Length <= 0 || extraFilters.Any( filter => string.IsNullOrEmpty( filter ) ) )
            {
                throw new ArgumentException( nameof( extraFilters ) );
            }

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return string.Empty;
            }

            var text = new string( value.Where( character => CheckValue( character ) ).ToArray() )
                .Replace( "'" , "" )
                .Replace( "\"" , "" )
                ;

            foreach ( var extraFilter in extraFilters )
            {
                text = text.Replace( extraFilter , "" );
            }

            return text.Trim( TrimCharacters );
        }
    }
}
