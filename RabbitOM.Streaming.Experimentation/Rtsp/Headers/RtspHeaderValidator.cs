using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal static class RtspHeaderValidator
    {
        private static readonly IReadOnlyCollection<char> ForbiddenCharacters = new HashSet<char>
        { 
            '$' , '£' , '€' ,
            'é' , 'è' , 'à' , 'ù' , 'ç' , 'µ' , 'ù' , '²' ,
            '¨' , '^' , '§' , '¤' , 
        };







        

        public static bool TryValidate( in char value )
        {
            return value > 31 && value < 127 && ! ForbiddenCharacters.Contains( value );
        }

        public static bool TryValidate( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return value.All( character => TryValidate( character ) );
        }
    }
}
