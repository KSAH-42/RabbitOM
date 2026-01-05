using System;

namespace RabbitOM.Streaming.Net.Rtcp.Messages
{
    public struct RtcpSourceDescriptionMessage
    {
        public uint SynchronizationSourceId { get; private set; }

        public RtcpSourceDescriptionItem[] Items { get; private set; }



        public static bool TryParse( in ArraySegment<byte> payload , out RtcpSourceDescriptionMessage result )
        {
            throw new NotImplementedException();
        }
    }
}
