using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class StringRtspHeaderValueExtensions 
    {
        public static string ToToken( this string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return string.Empty;
            }

            if ( ! value.Any( character => char.IsLetterOrDigit( character ) ) )
            {
                 return string.Empty;
            }

            return value;
        }
    }
}
