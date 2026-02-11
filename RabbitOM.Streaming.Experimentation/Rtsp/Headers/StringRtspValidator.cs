using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class StringRtspValidator
    {
        public static bool TryValidate( string value )
        {
            return ! string.IsNullOrWhiteSpace( value ) && ! value.Any( x => char.IsControl( x ) );
        }

        public static bool TryValidateUri( string value )
        {
            return Uri.IsWellFormedUriString( value , UriKind.Absolute );
        }
        
        public static bool TryValidateAsContentTD( string value )
        {
            return ! string.IsNullOrWhiteSpace( value )
                && value.Count( x => char.IsLetter( x ) ) > 0
                && value.Count( x => char.IsDigit( x ) ) >= 0
                ;
        }

        public static bool TryValidateAsContentSTD( string value )
        {
            // Star Text Digits
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            if ( value == "*" )
            {
                return true;
            }

            return value.Count( x => char.IsLetter( x ) ) > 0
                && value.Count( x => char.IsDigit( x ) ) >= 0
                ;
        }
    }
}
