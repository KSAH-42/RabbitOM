using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public struct RtcpApplicationPacket
    {
        public uint SynchronizationSourceId { get; private set; }

        public string Name { get; private set; }

        public string Data { get; private set; }



        public static bool TryParse( in ArraySegment<byte> payload , out RtcpSourceDescriptionPacket result )
        {
            throw new NotImplementedException();
        }
    }
}
