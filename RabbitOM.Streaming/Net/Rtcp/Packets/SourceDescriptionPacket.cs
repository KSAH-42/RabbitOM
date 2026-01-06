using System;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public sealed class SourceDescriptionPacket : RtcpPacket
    {
        public uint SynchronizationSourceId { get; private set; }
        
        public RtcpSourceDescriptionItem[] Items { get; private set; }




        public static bool TryParse( in ArraySegment<byte> buffer , out SourceDescriptionPacket result )
        {
            throw new NotImplementedException();
        }
    }
}
