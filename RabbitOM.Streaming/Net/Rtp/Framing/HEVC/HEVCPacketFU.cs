using System;

namespace RabbitOM.Streaming.Net.Rtp.Framing.HEVC
{
    public struct HEVCPacketFU
    {
        public bool Indicator { get; private set; }

        public bool StartBit { get; private set; }
        
        public bool StopBit { get; private set; }
        
        public byte Type { get; private set; }

        public ArraySegment<byte> Data { get; private set; }




        public bool TryValidate()
            => Data.Count >= 1;





        public static int CreateHeader( ref HEVCPacketFU fragmentedPacket , int packetHeader )
            => (fragmentedPacket.Type << 9) | (packetHeader & 0x81FF);



        public static bool IsStartPacket( ref HEVCPacketFU packet )
            => packet.StartBit && ! packet.StopBit;

        public static bool IsStopPacket( ref HEVCPacketFU packet )
            => ! packet.StartBit && packet.StopBit;

        public static bool IsIntermediaryPacket( ref HEVCPacketFU packet )
            => ! packet.StartBit && ! packet.StopBit;




        
        public static bool TryParse( ArraySegment<byte> buffer , out HEVCPacketFU result )
        {
            result = default;

            if ( buffer.Count <= 2 )
            {
                return false;
            }

            result = new HEVCPacketFU()
            {
                Indicator =   ( buffer.Array[ buffer.Offset     ]        & 0x1 ) == 1,
                StartBit  = ( ( buffer.Array[ buffer.Offset + 1 ] >> 7 ) & 0x1 ) == 1 ,
                StopBit   = ( ( buffer.Array[ buffer.Offset + 1 ] >> 6 ) & 0x1 ) == 1 ,
                Type      = (byte) (buffer.Array[ buffer.Offset + 1 ] & 0x3F ),
                Data      = new ArraySegment<byte>( buffer.Array , buffer.Offset + 2 , buffer.Array.Length - (buffer.Offset + 2) ),
            };

            return true;
        }
    } 
}