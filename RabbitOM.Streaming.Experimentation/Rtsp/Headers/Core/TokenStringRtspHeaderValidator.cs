using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Core
{
    public sealed class TokenStringRtspHeaderValidator : ProtocolStringRtspHeaderValidator
    {
        public override bool TryValidate( string value )
        {
            if ( base.TryValidate( value ) )
            {
                return value.Any( element => char.IsLetterOrDigit( element ) );
            }

            return false;
        }
    }
}
