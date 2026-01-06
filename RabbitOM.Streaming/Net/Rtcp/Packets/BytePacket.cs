using System;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public sealed class BytePacket : RtcpPacket
    {
        public uint[] SynchronizationSourcesIds { get; private set; }
        
        public string Reason { get; private set; }




        public static bool TryParse( in ArraySegment<byte> buffer , out BytePacket result )
        {
            throw new NotImplementedException();
        }
    }
}
