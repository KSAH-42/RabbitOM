using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public sealed class SenderReportPacket : RtcpPacket
    {
        public const int PacketType = 200;

        public const int MimimunSize = 24;
        



        private readonly List<RtcpReportBlock> _reports = new List<RtcpReportBlock>();



        public uint SynchronizationSourceId { get; private set; }
        
        public ulong NtpTimeStamp { get; private set; }
        
        public uint RtpTimeStamp { get; private set; }
        
        public uint NumberOfPackets { get; private set; }
        
        public uint NumberOfBytes { get; private set; }
        
        public IReadOnlyList<RtcpReportBlock> Reports { get => _reports; }




        private void AddReport( RtcpReportBlock report )
        {
            _reports.Add( report ?? throw new ArgumentNullException( nameof( report ) ) );
        }





        public static bool TryParse( RtcpMessage message , out SenderReportPacket result )
        {
            throw new NotImplementedException();
        }
    }
}
