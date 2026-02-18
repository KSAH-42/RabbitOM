using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class RtspValueNormalizer
    {
        private static readonly string[] ForbiddenCharacters = 
        { 
            "$" , "£" , "€" ,
            "é" , "è" , "à" , "ù" , "ç" , "µ" , "ù" , "²" ,
            "¨" , "^" , "§" , "¤" , "..." ,
            "{" ,  "}" , "<" , ">" ,
        };

        public static string Normalize( string value )
        {
            return Normalize( value , null );
        }

        public static string Normalize( string value , params string[] extraFilters )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return string.Empty;
            }

            var text = new string( value.Where( character => 31 < character && character < 127 ).ToArray() );

            foreach ( var filter in ForbiddenCharacters )
            {
                text = text.Replace( filter , "" );
            }

            if ( extraFilters != null )
            {
                foreach ( var extraFilter in extraFilters )
                {
                    text = text.Replace( extraFilter , "" );
                }
            }

            return text.Trim( '\"' , '\'' , ' ' );
        }
    }
}
