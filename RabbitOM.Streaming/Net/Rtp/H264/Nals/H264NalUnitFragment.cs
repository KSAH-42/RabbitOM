using System;

namespace RabbitOM.Streaming.Net.Rtp.H264.Nals
{
    /// <summary>
    /// Represent the H264 nal unit fragment
    /// </summary>
    public struct H264NalUnitFragment
    {
        /// <summary>
        /// Gets the start bit
        /// </summary>
        public bool StartBit { get; private set; }

        /// <summary>
        /// Gets the stop bit
        /// </summary>
        public bool StopBit { get; private set; }

        /// <summary>
        /// Gets the type
        /// </summary>
        public H264NalUnitType FragmentedType { get; private set; }

        /// <summary>
        /// Gets the payload
        /// </summary>
        public ArraySegment<byte> Payload { get; private set; }
        








        /// <summary>
        /// Check if it's a start packet
        /// </summary>
        /// <param name="nalUnit">the nal</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool IsStartPacket( in H264NalUnitFragment nalUnit )
            => nalUnit.StartBit && ! nalUnit.StopBit;

        /// <summary>
        /// Check if it's a stop packet
        /// </summary>
        /// <param name="nalUnit">the nal</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool IsStopPacket( in H264NalUnitFragment nalUnit )
            => ! nalUnit.StartBit && nalUnit.StopBit;

        /// <summary>
        /// Check if it's a data packet
        /// </summary>
        /// <param name="nalUnit">the nal</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool IsDataPacket( in H264NalUnitFragment nalUnit )
            => ! nalUnit.StartBit && ! nalUnit.StopBit;









        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( in ArraySegment<byte> buffer , out H264NalUnitFragment result )
        {
            result = default;

            if ( buffer.Count < 2 )
            {
                return false;
            }
            
            var header = buffer.Array[ buffer.Offset + 1 ];

            result = new H264NalUnitFragment();

            result.StartBit       = ( header >> 7 & 0x1 ) == 1;
            result.StopBit        = ( header >> 6 & 0x1 ) == 1;
            result.FragmentedType = (H264NalUnitType) ( header & 0x1F );

            if ( buffer.Count > 2 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 2 , buffer.Count - 2 );
            }

            return true;
        }

        /// <summary>
        /// Parse the header and reconstruct it
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte CreateHeader( in ArraySegment<byte> buffer )
        {
            if ( buffer.Count < 2 )
            {
                throw new ArgumentOutOfRangeException( nameof( buffer ) );
            }

            byte result = 0;

            result += (byte) (buffer.Array[ buffer.Offset ] & 0x80);
            result += (byte) (buffer.Array[ buffer.Offset ] & 0x60);
            result += (byte) (buffer.Array[ buffer.Offset + 1 ] & 0x1F);

            return result;
        }
    } 
}