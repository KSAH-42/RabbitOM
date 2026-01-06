using System;

namespace RabbitOM.Streaming.Net.Rtcp.Packets
{
    public sealed class ByteRtcpPacket : RtcpPacket
    {
        public uint[] SynchronizationSourcesIds { get; private set; }
        
        public string Reason { get; private set; }




        public static bool TryParse( in ArraySegment<byte> buffer , out ByteRtcpPacket result )
        {
            throw new NotImplementedException();
        }
    }
}
