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

            while ( text.StartsWith( "\"" ) || text.StartsWith( "'" ) || text.StartsWith( " " ) )
            {
                text = text.Remove( 0 , 1 );
            }

            while ( text.EndsWith( "\"" ) || text.EndsWith( "'" ) || text.EndsWith( " " ) )
            {
                text = text.Remove( text.Length - 1 , 1 );
            }

            return text;
        }
    }
}
