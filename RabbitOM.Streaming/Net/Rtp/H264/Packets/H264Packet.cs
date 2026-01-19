using System;

namespace RabbitOM.Streaming.Net.Rtp.H264.Packets
{
    public struct H264Packet
    {
        public H264PacketType Type { get; private set; }

        public static bool TryParse( in ArraySegment<byte> buffer , out H264Packet result )
        {
            result = default;

            if ( buffer.Count < 1 )
            {
                return false;
            }

            result = new H264Packet()
            { 
                Type = (H264PacketType) ( buffer.Array[ buffer.Offset ] & 0x1F ) 
            };

            return true;
        }
    } 
}