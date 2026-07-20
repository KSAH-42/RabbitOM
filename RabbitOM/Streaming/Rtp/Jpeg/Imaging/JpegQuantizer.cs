using System;

namespace RabbitOM.Streaming.Rtp.Jpeg.Imaging
{
    public static class JpegQuantizer
    {
        public static int AdaptFactor( int value )
        {
            value = value < 1 ? 1 : ( value > 99 ? 99 : value );

            return value < 50 ? ( 5000 / value ) : ( 200 - value * 2 );
        }

        public static byte Quantize( int value , int factor )
        {
            var newVal = ( value * factor + 50 ) / 100;

            return ( newVal < 1 ) ? (byte) 0x01 : ( newVal > 0xFF ) ? (byte) 0xFF : (byte) newVal;
        }
    }
}
