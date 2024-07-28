using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent a data converter
    /// </summary>
    public static class JpegDataConverter
    {
        /// <summary>
        /// Just adapt the quantization factor to an anther value
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>returns a values</returns>
        public static int ConvertToFactor( int value )
        {
            value = value < 1 ? 1 : ( value > 99 ? 99 : value );

            return value < 50 ? ( 5000 / value ) : ( 200 - value * 2 );
        }
    }
}
