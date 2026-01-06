using System;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public sealed class ApplicationPacket : RtcpPacket
    {
        public const int PacketType = 204;



        public uint SynchronizationSourceId { get; private set; }
        
        public string Name { get; private set; }
        
        public string Data { get; private set; }




        public static bool TryParse( RtcpMessage message , out ApplicationPacket result )
        {
            throw new NotImplementedException();
        }
    }
}
