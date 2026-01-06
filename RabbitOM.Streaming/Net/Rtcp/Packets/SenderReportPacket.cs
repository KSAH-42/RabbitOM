using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public sealed class SenderReportPacket : RtcpPacket
    {
        public const int PacketType = 200;

        public const int MimimunSize = 24;
        




        public uint SynchronizationSourceId { get; private set; }
        
        public ulong NtpTimeStamp { get; private set; }
        
        public uint RtpTimeStamp { get; private set; }
        
        public uint NumberOfPackets { get; private set; }
        
        public uint NumberOfBytes { get; private set; }
        
        public IReadOnlyList<RtcpReportBlock> Reports { get; private set; }




        public static bool TryParse( RtcpMessage message , out SenderReportPacket result )
        {
            throw new NotImplementedException();
        }
    }
}
