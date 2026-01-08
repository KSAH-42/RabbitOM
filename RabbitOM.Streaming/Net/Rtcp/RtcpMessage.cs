using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    /// <summary>
    /// Represent an rtcp message
    /// </summary>
    public sealed class RtcpMessage
    {
        /// <summary>
        /// Initialize a new instance of rtcp message class
        /// </summary>
        private RtcpMessage()
        {
        }

        /// <summary>
        /// Initialize a new instance of rtcp message class
        /// </summary>
        /// <param name="version">the version</param>
        /// <param name="padding">the padding</param>
        /// <param name="specificParameter">the specific parameter</param>
        /// <param name="type">the type</param>
        /// <param name="length">the length</param>
        /// <param name="payload">the payload</param>
        public RtcpMessage( byte version , bool padding , byte specificParameter , byte type , ushort length , ArraySegment<byte> payload )
        {
            Version = version;
            Padding = padding;
            SpecificParameter = specificParameter;
            Type = type;
            Length = length;
            Payload = payload;
        }
        





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
        /// Gets the length
        /// </summary>
        public ushort Length { get; private set; }
        
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
            result.Type              =            buffer.Array[ buffer.Offset + 1 ];
            result.Length            = (ushort) ( buffer.Array[ buffer.Offset + 2 ] * 0x100 + buffer.Array[ buffer.Offset + 3 ]);

            if ( buffer.Count >= 5 )
            {    
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 4 , Math.Min( result.Length , ( buffer.Array.Length - ( buffer.Offset + 4 ) ) ) );
            }

            return true;
        }
    }
}
