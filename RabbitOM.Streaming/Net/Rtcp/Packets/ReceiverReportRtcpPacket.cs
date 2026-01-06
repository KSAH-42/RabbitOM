using System;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public sealed class ReceiverReportRtcpPacket : RtcpPacket
    {
        public uint SynchronizationSourceId { get; private set; }
        
        public RtcpReportBlock[] Reports { get; private set; }




        public static bool TryParse( in ArraySegment<byte> buffer , out ReceiverReportRtcpPacket result )
        {
            throw new NotImplementedException();
        }
    }
}
