using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    // TODO: does header must contains it's own token validation an expose a public static method called IsValidToken ?
    // or used centralized approach component even if there is some duplicated code in some headers ?
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
