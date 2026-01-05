using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public struct RtcpByePacket
    {
        public uint[] SynchronizationSourcesIds { get; private set; }

        public string Option { get; private set; }



        public static bool TryParse( in ArraySegment<byte> payload , int count , out RtcpByePacket result )
        {
            throw new NotImplementedException();
        }
    }
}
