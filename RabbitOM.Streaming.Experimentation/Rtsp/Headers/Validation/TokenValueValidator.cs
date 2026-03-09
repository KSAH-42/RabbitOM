using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation
{
    public sealed class TokenValueValidator : StringValueValidator
    {
        private static readonly IReadOnlyCollection<char> InvalidChars = new HashSet<char>() { '²' , 'é' , '~' , 'ç' , 'è' , '$' , '£' , '€' , '¤' , '¨' , 'µ' , 'ù' , '^' , '§'  };
        
        public override bool TryValidate( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var succeed = false;

            foreach ( var element in value )
            {
                if ( element <= 31 || element >= 127 || char.IsControl( element ) )
                {
                    return false;
                }

                if ( InvalidChars.Contains( element ) )
                {
                    return false;
                }

                succeed |= char.IsLetterOrDigit( element ) || element == '*' ;
            }

            return succeed;
        }
    }
}
