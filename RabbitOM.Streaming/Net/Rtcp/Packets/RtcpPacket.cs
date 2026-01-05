using System;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    /// <summary>
    /// Represent an rtcp packet
    /// </summary>
    public struct RtcpPacket
    {
        /// <summary>
        /// Gets Version
        /// </summary>
        public byte Version { get; private set; }

        /// <summary>
        /// Gets the padding flag
        /// </summary>
        public bool Padding { get; private set; }
        
        /// <summary>
        /// Gets the number of element count stored on the payload
        /// </summary>
        public byte Count { get; private set; }
        
        /// <summary>
        /// Gets the type
        /// </summary>
        public RtcpPacketType Type { get; private set; }
        
        /// <summary>
        /// Gets the length
        /// </summary>
        public ushort Length { get;private set; }
        
        /// <summary>
        /// Gets the payload
        /// </summary>
        public ArraySegment<byte> Payload { get; private set; }






        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="buffer">the buffer</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( in ArraySegment<byte> buffer , out RtcpPacket result )
        {
            result = default;

            if ( buffer.Count < 4 )
            {
                return false;
            }

            result = new RtcpPacket();
            
            result.Version   = (byte) ((buffer.Array[ buffer.Offset ] >> 6) & 0x3);

            result.Padding   = ((buffer.Array[ buffer.Offset ] >> 5) & 0x1) == 1;

            result.Count     = (byte) (buffer.Array[ buffer.Offset ] & 0x1F);

            result.Type      = (RtcpPacketType) buffer.Array[ buffer.Offset + 1 ];

            result.Length    = (ushort) ( buffer.Array[ buffer.Offset + 2 ] * 0x100 + buffer.Array[ buffer.Offset + 3 ]);

            if ( buffer.Count >= 5 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 4 , buffer.Array.Length - ( buffer.Offset + 4 ) );
            }

            return true;
        }
    }
}
