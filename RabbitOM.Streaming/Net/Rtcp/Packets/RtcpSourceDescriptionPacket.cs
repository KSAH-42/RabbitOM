using System;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public struct RtcpSourceDescriptionPacket
    {
        public uint SynchronizationSourceId { get; private set; }

        public RtcpSourceDescriptionItem[] Items { get; private set; }



        public static bool TryParse( in ArraySegment<byte> payload , out RtcpSourceDescriptionPacket result )
        {
            throw new NotImplementedException();
        }
    }
}
