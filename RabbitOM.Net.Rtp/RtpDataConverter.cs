using System;

namespace RabbitOM.Net.Rtp
{
    /// <summary>
    /// Represent a data converter
    /// </summary>
    public static class RtpDataConverter
    {
        /// <summary>
        /// Convert
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="offset">the offset</param>
        /// <returns>returns a value</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static int ConvertToInt16( byte[] buffer , int offset )
        {
            if ( buffer == null )
            {
                throw new ArgumentNullException( nameof( buffer ) );
            }

            if ( buffer.Length <= 0 || buffer.Length <= ( offset + 1 ) )
            {
                throw new ArgumentOutOfRangeException( nameof( buffer ) );
            }

            return ( buffer[ offset ] << 8 ) | buffer[ ++ offset ];
        }

        /// <summary>
        /// Convert
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="offset">the offset</param>
        /// <returns>returns a value</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static int ConvertToInt24( byte[] buffer , int offset )
        {
            if ( buffer == null )
            {
                throw new ArgumentNullException( nameof( buffer ) );
            }

            if ( buffer.Length <= 0 || buffer.Length <= ( offset + 2 ) )
            {
                throw new ArgumentOutOfRangeException( nameof( buffer ) );
            }

            return buffer[    offset ] << 16 |
                   buffer[ ++ offset ] << 8 |
                   buffer[ ++ offset ];
        }

        /// <summary>
        /// Convert
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="offset">the offset</param>
        /// <returns>returns a value</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static uint ConvertToUInt( byte[] buffer , int offset )
        {
            if ( buffer == null )
            {
                throw new ArgumentNullException( nameof( buffer ) );
            }

            if ( buffer.Length <= 0 || buffer.Length <= ( offset + 3 ) )
            {
                throw new ArgumentOutOfRangeException( nameof( buffer ) );
            }

            return (uint) ( buffer[    offset ] << 24 |
                            buffer[ ++ offset ] << 16 |
                            buffer[ ++ offset ] << 8 |
                            buffer[ ++ offset ] );
        }
    }
}
