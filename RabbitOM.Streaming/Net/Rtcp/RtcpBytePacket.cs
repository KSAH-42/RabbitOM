using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public struct RtcpBytePacket
    {
        public uint[] SynchronizationSourcesIds { get; private set; }





        public static RtcpBytePacket Parse( in ArraySegment<byte> payload , int count )
        {
            throw new NotImplementedException();
        }
    }
}
