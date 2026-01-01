using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H264.Nals
{
    /// <summary>
    /// Represent the H264 nal unit
    /// </summary>
    /// <seealso cref="https://datatracker.ietf.org/doc/html/rfc6184#section-1.3"/>
    public struct H264NalUnit
    {
        /// <summary>
        /// Gets the forbidden bit
        /// </summary>
        public bool ForbiddenBit { get; private set; }

        /// <summary>
        /// Gets the type
        /// </summary>
        public H264NalUnitType Type { get; private set; }

        /// <summary>
        /// Gets the Nri
        /// </summary>
        public byte Nri { get; private set; }

        /// <summary>
        /// Gets the payload
        /// </summary>
        public ArraySegment<byte> Payload { get; private set; }
        
               


        

        
        /// <summary>
        /// Try to parse the type
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( in ArraySegment<byte> buffer , out H264NalUnitType result )
        {
            result = H264NalUnitType.UNKNOWN;

            if ( buffer.Count < 1 )
            {
                return false;
            }

            result = (H264NalUnitType) ( buffer.Array[ buffer.Offset ] & 0x1F );

            return true;
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( in ArraySegment<byte> buffer , out H264NalUnit result )
        {
            result = default;

            if ( buffer.Count < 1 )
            {
                return false;
            }

            var header = buffer.Array[ buffer.Offset ];
            
            result = new H264NalUnit();

            result.Type          = (H264NalUnitType) ( header  & 0x1F );
            result.ForbiddenBit  = (byte) ( ( header >> 7 ) & 0x01 ) == 1;
            result.Nri           = (byte) ( ( header >> 5 ) & 0x03 );

            if ( buffer.Count > 1 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 1 , buffer.Array.Length - ( buffer.Offset + 1 ) );
            }

            return true;
        }

        /// <summary>
        /// Parse aggregates
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <returns>returns a collection of nal aggregates</returns>
        public static IList<ArraySegment<byte>> ParseAggregates( in ArraySegment<byte> buffer )
        {
            var results = new List<ArraySegment<byte>>();

            var index = buffer.Offset + 1;
            
            while ( index < buffer.Array.Length - 2 )
            {
                var size = buffer.Array[ index ++ ] * 0x100 | buffer.Array[ index ++ ];

                if ( 0 < size && size <= (buffer.Array.Length - (buffer.Offset + index)) )
                {
                    results.Add( new ArraySegment<byte>( buffer.Array , index , size ) );
                }

                index += size ;
            }

            return results;
        }
    } 
}