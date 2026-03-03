using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed class RtspHeaderTokenValidator
    {
        public bool ContainsLetterOrDigit( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            return value.Any( element => char.IsLetterOrDigit( element ) );
        }
    }
}
