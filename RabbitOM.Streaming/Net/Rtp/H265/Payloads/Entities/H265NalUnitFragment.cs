using System;

namespace RabbitOM.Streaming.Net.Rtp.H265.Payloads.Entities
{
    /// <summary>
    /// Represent a H265 fragmented nalu <seealso cref="https://datatracker.ietf.org/doc/html/rfc7798#section-4.4.3"/>
    /// </summary>
    public struct H265NalUnitFragment
    {
        /// <summary>
        /// Gets ther forbidden bit
        /// </summary>
        public bool ForbiddenBit { get; private set; }

        /// <summary>
        /// Gets the start bit
        /// </summary>
        public bool StartBit { get; private set; }

        /// <summary>
        /// Gets the stop bit
        /// </summary>
        public bool StopBit { get; private set; }

        /// <summary>
        /// Gets the fragmented type
        /// </summary>
        public byte FragmentedType { get; private set; }

        /// <summary>
        /// Gets the optional payload that can be null
        /// </summary>
        public ArraySegment<byte> Payload { get; private set; }
        





        /// <summary>
        /// Check if the nal start packet
        /// </summary>
        /// <param name="nalUnit">the nalu</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool IsStartPacket( in H265NalUnitFragment nalUnit )
            => nalUnit.StartBit && ! nalUnit.StopBit;

        /// <summary>
        /// Check if the nal packet between a start and a stop
        /// </summary>
        /// <param name="nalUnit">the nalu</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool IsStopPacket( in H265NalUnitFragment nalUnit )
            => ! nalUnit.StartBit && nalUnit.StopBit;

        /// <summary>
        /// Check if the nal stop packet
        /// </summary>
        /// <param name="nalUnit">the nalu</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool IsDataPacket( in H265NalUnitFragment nalUnit )
            => ! nalUnit.StartBit && ! nalUnit.StopBit;
        





        
        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="buffer">the rtp payload</param>
        /// <param name="donl">the flag for decoded order number</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( in ArraySegment<byte> buffer , bool donl , out H265NalUnitFragment result )
        {
            result = default;

            if ( buffer.Count < 3 )
            {
                return false;
            }
            
            result = new H265NalUnitFragment();

            result.ForbiddenBit   =        ( ( buffer.Array[ buffer.Offset + 0 ] >> 7 ) & 0x1  ) == 1;
            result.StartBit       =        ( ( buffer.Array[ buffer.Offset + 2 ] >> 7 ) & 0x1  ) == 1;
            result.StopBit        =        ( ( buffer.Array[ buffer.Offset + 2 ] >> 6 ) & 0x1  ) == 1;
            result.FragmentedType = (byte) ( ( buffer.Array[ buffer.Offset + 2 ]      ) & 0x3F );
    
            if ( buffer.Count > 3 )
            {
                var count = donl ? 5 : 3;

                if ( buffer.Count - count < 0 )
                {
                    return false;
                }

                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + count , buffer.Count - count );
            }

            return true;
        }

        /// <summary>
        /// Parse the nal header and reconstruct it
        /// </summary>
        /// <param name="buffer">the rtp payload</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static int ReConstructHeader( in ArraySegment<byte> buffer )
        {
            if ( buffer.Count < 3 )
            {
                throw new ArgumentOutOfRangeException( nameof( buffer ) );
            }

            var result = 0x81FF & ( buffer.Array[ buffer.Offset ] << 8 ) | ( buffer.Array[ buffer.Offset + 1 ] );

            return result |= ( (byte) ( buffer.Array[ buffer.Offset + 2 ] & 0x3F ) << 9 );
        }
    } 
}