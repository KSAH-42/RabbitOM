using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    /// <summary>
    /// Represent an rtcp message
    /// </summary>
    public sealed class RtcpMessage
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
        /// Gets the parameter (it can be RC, or ST, etc...)
        /// </summary>
        public byte SpecificParameter { get; private set; }
        
        /// <summary>
        /// Gets the type
        /// </summary>
        public byte Type { get; private set; }
        
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
        public static bool TryParse( in ArraySegment<byte> buffer , out RtcpMessage result )
        {
            result = default;

            if ( buffer.Count < 4 )
            {
                return false;
            }

            result = new RtcpMessage();
            
            result.Version           = (byte) ( ( buffer.Array[ buffer.Offset ] >> 6 ) & 0x3 );
            result.Padding           =        ( ( buffer.Array[ buffer.Offset ] >> 5 ) & 0x1 ) == 1;
            result.SpecificParameter = (byte) (   buffer.Array[ buffer.Offset ] & 0x1F );
            result.Type              = (byte)     buffer.Array[ buffer.Offset + 1 ];

            if ( buffer.Count >= 5 )
            {
                var length = (ushort) ( buffer.Array[ buffer.Offset + 2 ] * 0x100 + buffer.Array[ buffer.Offset + 3 ]);
                
                var count  = ( buffer.Array.Length - ( buffer.Offset + 4 ) );
                
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 4 , Math.Min( count , length ) );
            }

            return true;
        }
    }
}
