using System;

namespace RabbitOM.Streaming.Net.Rtp.H265.Payloads.Types
{
    /// <summary>
    /// Represent a H265 nalu
    /// </summary>
    /// <seealso cref="https://datatracker.ietf.org/doc/html/rfc7798#section-1.1.4"/>
    /// <remarks>
    /// <para>
    ///     +---------------+---------------+
    ///     |7|6|5|4|3|2|1|0|7|6|5|4|3|2|1|0|
    ///     +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///     |F|   Type    |  LayerId  | TID |
    ///     +-------------+-----------------+
    /// </para>
    /// </remarks>
    public struct H265NalUnit
    {
        /// <summary>
        /// Gets the forbidden bit
        /// </summary>
        public bool ForbiddenBit { get; private set; }

        /// <summary>
        /// Gets the type
        /// </summary>
        public byte Type { get; private set; }

        /// <summary>
        /// Gets the layer identifier
        /// </summary>
        public byte LayerId { get; private set; }

        /// <summary>
        /// Gets the temporal identifier
        /// </summary>
        public byte TemportalId { get; private set; }

        /// <summary>
        /// Gets the optional payload that can be null
        /// </summary>
        public ArraySegment<byte> Payload { get; private set; }
        



        /// <summary>
        /// Verify the forbidden bit
        /// </summary>
        /// <param name="buffer">the buffer to be parsed</param>
        /// <returns>returns true if no buffer, or if the bit status is set to true</returns>
        public static bool IsNullOrForbidden( in ArraySegment<byte> buffer )
            => buffer.Count <= 0 || ( ( buffer.Array[ buffer.Offset ] >> 7 ) & 0x01 ) == 1;
        



        
        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="buffer">the rtp payload</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( in ArraySegment<byte> buffer , out H265NalUnit result )
        {
            result = default;

            if ( buffer.Count < 2 )
            {
                return false;
            }
            
            var header = ( buffer.Array[ buffer.Offset ] << 8 ) | ( buffer.Array[ buffer.Offset + 1 ] );
            
            result = new H265NalUnit();

            result.ForbiddenBit  = (byte) ( ( header >> 15) & 0x01 ) == 1;
            result.Type          = (byte) ( ( header >> 9 ) & 0x3F );
            result.LayerId       = (byte) ( ( header >> 3 ) & 0x3F );
            result.TemportalId   = (byte) (   header        & 0x07 );

            if ( buffer.Count > 2 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 2 , buffer.Count - 2 );
            }

            return true;
        }  
    } 
}