using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public sealed class SourceDescriptionPacket : RtcpPacket
    {
        public const int PacketType = 202;




        public uint SynchronizationSourceId { get; private set; }
        
        public IReadOnlyList<RtcpSourceDescriptionItem> Items { get; private set; }




        public static bool TryParse( RtcpMessage message , out SourceDescriptionPacket result )
        {
            throw new NotImplementedException();
        }
    }
}
