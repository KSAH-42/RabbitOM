using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public static class JpegDataConverter
    {
        public static int ConvertToFactor( int value )
        {
            value = value < 1 ? 1 : ( value > 99 ? 99 : value );

            return value < 50 ? ( 5000 / value ) : ( 200 - value * 2 );
        }
    }
}
