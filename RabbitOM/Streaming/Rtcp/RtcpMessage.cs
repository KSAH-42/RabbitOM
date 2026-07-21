using System;

namespace RabbitOM.Streaming.Rtcp
{
    public sealed class RtcpMessage
    {
        private RtcpMessage()
        {
        }

        public RtcpMessage( byte version , bool padding , byte specificParameter , byte type , ushort length , ArraySegment<byte> payload )
        {
            Version = version;
            Padding = padding;
            SpecificParameter = specificParameter;
            Type = type;
            Length = length;
            Payload = payload;
        }




        public byte Version { get; private set; }

        public bool Padding { get; private set; }

        public byte SpecificParameter { get; private set; }

        public byte Type { get; private set; }

        public ushort Length { get; private set; }

        public ArraySegment<byte> Payload { get; private set; }




        public static bool TryParse( ArraySegment<byte> buffer , out RtcpMessage result )
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
