using System;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed class RtspHeaderFormatter
    {
        private static readonly char[] TrimCharacters =
        {
            ' ',
            ',',
            ';',
        };

        private static readonly char[] TrimQuotesChars = 
        { 
            ' ' , 
            '\'' , 
            '\"' 
        };







        public string Format( long value )
        {
            return value.ToString();
        }

        public string Format( double value )
        {
            return value.ToString( "F3" , CultureInfo.InvariantCulture );
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
            
            return $"{value.Trim( TrimQuotesChars )}";
        }


        






        // trim only space, coma and semi-colon and never quotes
        public string Filter( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return string.Empty;
            }

            return new string( value.Where( character => RtspHeaderValidator.TryValidate( character ) ).ToArray() )
                //.Replace( "'" , "" )
                //.Replace( "\"" , "" )
                //.Trim( TrimCharacters )
                .Trim()
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

            var text = new string( value.Where( character => RtspHeaderValidator.TryValidate( character ) ).ToArray() )
                //.Replace( "'" , "" )
                //.Replace( "\"" , "" )
                .Trim()
                ;

            foreach ( var occurence in occurences )
            {
                text = text.Replace( occurence , "" );
            }

            return text.Trim( TrimCharacters );
        }
    }
}
