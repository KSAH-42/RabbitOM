using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class RtspHeaderFormatter
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








        public bool CheckValue( char value )
        {
            return value > 31 && value < 127 && ! ForbiddenCharacters.Contains( value );
        }

        public bool CheckValue( string value )
        {
            return value?.All( CheckValue ) ?? false;
        }








        public string Format( long value )
        {
            return value.ToString();
        }

        public string Format( double value )
        {
            return string.Format( CultureInfo.InvariantCulture , "{0:F3}" , value );
        }

        public string Format( float value )
        {
            return value.ToString( "G2" , CultureInfo.InvariantCulture );
        }

        public string Format( DateTime value )
        {
            value = value.Kind == DateTimeKind.Local ? value.ToUniversalTime() : value;

            return value.ToString( "r" , CultureInfo.InvariantCulture );
        }







        // handle case where quotes are already present on borders at multiple times
        public string Quote( string value )
        {
            return $"\"{UnQuote(value)}\"";
        }

        // remove quote only on borders
        public string UnQuote( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return string.Empty;
            }
            
            return $"{value.Trim( TrimCharacters )}";
        }


        






        // trim only space, coma and semi-colon and never quotes
        public string Filter( string value )
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

        public string Filter( string value , params string[] occurences )
        {
            if ( occurences == null )
            {
                throw new ArgumentNullException( nameof( occurences) );
            }

            if ( occurences.Length <= 0 || occurences.Any( filter => string.IsNullOrEmpty( filter ) ) )
            {
                throw new ArgumentException( nameof( occurences ) );
            }

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return string.Empty;
            }

            var text = new string( value.Where( character => CheckValue( character ) ).ToArray() )
                .Replace( "'" , "" )
                .Replace( "\"" , "" )
                ;

            foreach ( var occurence in occurences )
            {
                text = text.Replace( occurence , "" );
            }

            return text.Trim( TrimCharacters );
        }
    }
}
