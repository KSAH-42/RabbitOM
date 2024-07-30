using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent a quantizer helper class
    /// </summary>
    public static class JpegQuantizer
    {
        /// <summary>
        /// Just adapt the quantization factor to an anther value
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>returns a values</returns>
        public static int AdaptFactor( int value )
        {
            value = value < 1 ? 1 : ( value > 99 ? 99 : value );

            return value < 50 ? ( 5000 / value ) : ( 200 - value * 2 );
        }

        /// <summary>
        /// Just transform an input value (it reduce the precision of the value)
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="factor">the quantization factor</param>
        /// <returns>returns a value</returns>
        public static byte Quantize( int value , int factor )
        {
            int newVal = ( value * factor + 50 ) / 100;

            return ( newVal < 1 ) ? (byte) 0x01 : ( newVal > 0xFF ) ? (byte) 0xFF : (byte) newVal;
        }
    }
}
