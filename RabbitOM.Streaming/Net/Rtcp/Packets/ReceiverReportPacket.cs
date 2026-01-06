using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public sealed class ReceiverReportPacket : RtcpPacket
    {
        public const int PacketType = 201;



        private readonly List<RtcpReportBlock> _reports = new List<RtcpReportBlock>();




        public uint SynchronizationSourceId { get; private set; }
        
        public IReadOnlyList<RtcpReportBlock> Reports { get => _reports; }





        private void AddReport( RtcpReportBlock report )
        {
            _reports.Add( report ?? throw new ArgumentNullException( nameof( report ) ) );
        }




        public static bool TryParse( RtcpMessage message , out ReceiverReportPacket result )
        {
            throw new NotImplementedException();
        }
    }
}
