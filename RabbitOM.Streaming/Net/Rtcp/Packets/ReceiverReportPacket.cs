using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public sealed class ReceiverReportPacket : RtcpPacket
    {
        public const int PacketType = 201;




        public uint SynchronizationSourceId { get; private set; }
        
        public IReadOnlyList<RtcpReportBlock> Reports { get; private set; }




        public static bool TryParse( in ArraySegment<byte> buffer , out ReceiverReportPacket result )
        {
            throw new NotImplementedException();
        }
    }
}
