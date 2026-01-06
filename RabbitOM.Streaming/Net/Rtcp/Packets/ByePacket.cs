using System;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public sealed class ByePacket : RtcpPacket
    {
        public const int PacketType = 203;




        public uint[] SynchronizationSourcesIds { get; private set; }
        
        public string Reason { get; private set; }




        public static bool TryParse( in ArraySegment<byte> buffer , out ByePacket result )
        {
            throw new NotImplementedException();
        }
    }
}
