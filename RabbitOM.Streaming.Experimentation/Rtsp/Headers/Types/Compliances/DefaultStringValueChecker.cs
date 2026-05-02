using System;
using System.Collections.Generic;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances
{
    public sealed class DefaultStringValueChecker : StringValueChecker 
    {
        private static readonly HashSet<char> InvalidChars = new HashSet<char>() { '²' , 'é' , '~' , 'ç' , 'è' , '$' , '£' , '€' , '¤' , '¨' , 'µ' , 'ù' , '^' , '§'  , '[' , ']' , '{' , '}' , '<' , '>' };

        public override bool CheckValue( string value )
        {
            foreach ( var character in value ?? string.Empty )
            {
                if ( character <= 31 || character >= 127 || char.IsControl( character ) || InvalidChars.Contains( character ) )
                {
                    return false;
                }
            }

            return true;
        }

        public override string EnsureValue( string value )
        {
            foreach ( var character in value ?? string.Empty )
            {
                if ( character <= 31 || character >= 127 || char.IsControl( character ) || InvalidChars.Contains( character ) )
                {
                    throw new InvalidDataException( $"the value contains bad characters : {value}" );
                }
            }

            return value ?? string.Empty;
        }
    }
}
