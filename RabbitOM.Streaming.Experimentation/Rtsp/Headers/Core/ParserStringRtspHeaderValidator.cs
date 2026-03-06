using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core
{
    public class ProtocolStringRtspHeaderValidator : StringRtspHeaderValidator
    {
        private static readonly IReadOnlyCollection<char> InvalidChars = new HashSet<char>() { '²' , 'é' , '~' , 'ç' , 'è' , '$' , '£' , '€' , '¤' , '¨' , 'µ' , 'ù' , '^' , '§' , '\r' , '\n' , '\t' , '\f' , '\v'  };
        
        public override bool TryValidate( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

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
            }

            return true;
        }
    }
}
