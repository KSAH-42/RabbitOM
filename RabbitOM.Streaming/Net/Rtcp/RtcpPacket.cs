using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public struct RtcpPacket
    {
        public byte Version { get; private set; }
        public bool Padding { get; private set; }
        public byte ReceptionCount { get; private set; }
        public RtcpPacketType Type { get; private set; }
        public ushort Length { get;private set; }
        public ArraySegment<byte> Payload { get; private set; }



        public static bool IsInvalidOrUnDefined( ref RtcpPacket packet )
        {
            return packet.Version == 0 || packet.Type == RtcpPacketType.UNDEFINED || packet.Payload.Count == 0;
        }




        /*
          7 6  5  4 3 2 1 0 
         +----------------+
         | V | P |   RC   |
         +----------------+
         */
        public static bool TryParse( ArraySegment<byte> buffer , out RtcpPacket result )
        {
            result = default;

            if ( buffer.Count < 4 )
            {
                return false;
            }

            result = new RtcpPacket();
            
            result.Version        = (byte) ((buffer.Array[ buffer.Offset ] >> 6) & 0x3);

            result.Padding        = ((buffer.Array[ buffer.Offset ] >> 5) & 0x1) == 1;

            result.ReceptionCount = (byte) (buffer.Array[ buffer.Offset ] & 0x1F);

            result.Type           = (RtcpPacketType) buffer.Array[ buffer.Offset + 1 ];

            result.Length         = (ushort) ( buffer.Array[ buffer.Offset + 2 ] * 0x100 + buffer.Array[ buffer.Offset + 3 ]);

            if ( buffer.Count >= 5 )
            {
                result.Payload = new ArraySegment<byte>( buffer.Array , buffer.Offset + 4 , buffer.Array.Length - ( buffer.Offset + 4 ) );
            }

            return true;
        }
    }
}
