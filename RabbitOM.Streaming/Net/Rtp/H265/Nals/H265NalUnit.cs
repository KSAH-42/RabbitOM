using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H265.Nals
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
        public H265NalUnitType Type { get; private set; }

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
        /// Parse the type
        /// </summary>
        /// <param name="buffer">the rtp payload</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( in ArraySegment<byte> buffer , out H265NalUnitType result )
        {
            result = H265NalUnitType.UNKNOWN;

            if ( buffer.Count < 2 )
            {
                return false;
            }

            result = (H265NalUnitType) ( ( buffer.Array[ buffer.Offset ] >> 1 ) & 0x3F );

            return true;
        }

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

            result.ForbiddenBit  = (byte)            ( ( header >> 15) & 0x01 ) == 1;
            result.Type          = (H265NalUnitType) ( ( header >> 9 ) & 0x3F );
            result.LayerId       = (byte)            ( ( header >> 3 ) & 0x3F );
            result.TemportalId   = (byte)            (   header        & 0x07 );

            if ( buffer.Count > 2 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 2 , buffer.Count - 2 );
            }

            return true;
        }

        /// <summary>
        /// Parse aggregates
        /// </summary>
        /// <param name="buffer">the rtp payload</param>
        /// <param name="donl">the donl</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static IList<ArraySegment<byte>> ParseAggregates( in ArraySegment<byte> buffer , bool donl )
        {
            var results = new List<ArraySegment<byte>>();

            var index = buffer.Offset + 2;
            
            while ( ( index += ( donl ? 2 : 0 ) ) < buffer.Array.Length - 2 )
            {
                var size = buffer.Array[ index ++ ] * 0x100 | buffer.Array[ index ++ ];

                if ( 0 < size && size <= (buffer.Array.Length - (buffer.Offset + index)) )
                {
                    results.Add( new ArraySegment<byte>( buffer.Array , index , size ) );
                }

                index += size;
            }

            return results;
        }       
    } 
}