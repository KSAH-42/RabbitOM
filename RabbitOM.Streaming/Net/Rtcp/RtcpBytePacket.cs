using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public struct RtcpBytePacket
    {
        public uint[] SynchronizationSourcesIds { get; private set; }

        public string Option { get; private set; }



        public static bool TryParse( in ArraySegment<byte> payload , int count , out RtcpBytePacket result )
        {
            throw new NotImplementedException();
        }
    }
}
