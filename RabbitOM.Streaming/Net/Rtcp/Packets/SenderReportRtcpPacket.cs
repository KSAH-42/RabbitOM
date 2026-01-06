using System;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public sealed class SenderReportRtcpPacket : RtcpPacket
    {
        public const int MimimunSize = 24;
        



        public uint SynchronizationSourceId { get; private set; }
        
        public ulong NtpTimeStamp { get; private set; }
        
        public uint RtpTimeStamp { get; private set; }
        
        public uint PacketCount { get; private set; }
        
        public uint OctetCount { get; private set; }
        
        public RtcpReportBlock[] Reports { get; private set; }




        public static bool TryParse( in ArraySegment<byte> buffer , out SenderReportRtcpPacket result )
        {
            throw new NotImplementedException();
        }
    }
}
