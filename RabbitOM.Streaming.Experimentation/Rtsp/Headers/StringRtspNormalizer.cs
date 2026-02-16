using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class StringRtspNormalizer
    {
        public static string Normalize( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return string.Empty;
            }

            var text = new string( value.Where( charatecter => ! char.IsControl( charatecter ) ).ToArray() );

            return text.Trim( new char[] { '\"' , '\'' , ' ' } );
        }
    }
}
