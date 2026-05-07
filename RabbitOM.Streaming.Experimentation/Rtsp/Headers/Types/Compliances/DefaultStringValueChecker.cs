using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types.Compliances
{
    public sealed class DefaultStringValueChecker : StringValueChecker 
    {
        private readonly static IReadOnlyCollection<char> SpecialAllowedChars = new HashSet<char>() { '.','!','$','|','_','-','*','&','~','%' };

        public override bool CheckValue( string value )
        {
            return OnCheckValue( value );
        }

        public override string EnsureValue( string value )
        {
            return OnCheckValue( value ) ? value : throw new FormatException();
        }




        private bool OnCheckValue( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            foreach ( var character in value ?? string.Empty )
            {
                if ( char.IsLetterOrDigit( character ) || SpecialAllowedChars.Contains( character ) )
                {
                    continue;
                }

                if ( char.IsControl( character ) || InvalidChars.Contains( character ) )
                {
                    return false;
                }

                if ( character <= 31 || character >= 127 )
                {
                    return false;
                }
            }

            return true;
        }
    }
}
