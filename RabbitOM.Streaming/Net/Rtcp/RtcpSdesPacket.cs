using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public struct RtcpSdesPacket
    {
        public uint SynchronizationSourceId { get; private set; }

        public long TimeStamp { get; private set; }




        public static RtcpSdesPacket Parse( in ArraySegment<byte> payload )
        {
            throw new NotImplementedException();
        }
    }
}
