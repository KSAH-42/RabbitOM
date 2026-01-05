using System;

namespace RabbitOM.Streaming.Net.Rtcp.Payloads
{
    public struct RtcpSourceDescriptionPayload
    {
        public uint SynchronizationSourceId { get; private set; }

        public RtcpSourceDescriptionItem[] Items { get; private set; }



        public static bool TryParse( in ArraySegment<byte> payload , out RtcpSourceDescriptionPayload result )
        {
            throw new NotImplementedException();
        }
    }
}
