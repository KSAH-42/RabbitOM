using System;
using System.Linq;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public static class StringRtspValidator
    {
        public static bool TryValidate( string value )
        {
            return ! string.IsNullOrWhiteSpace( value ) && ! value.Any( x => char.IsControl( x ) );
        }

        public static bool TryValidateUri( string value )
        {
            return Uri.IsWellFormedUriString( value , UriKind.RelativeOrAbsolute );
        }
    }
}
