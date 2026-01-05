using System;

namespace RabbitOM.Streaming.Net.Rtcp.Messages
{
    public struct RtcpByeMessage
    {
        public uint[] SynchronizationSourcesIds { get; private set; }
        
        public string Reason { get; private set; }

        public static bool TryParse( in ArraySegment<byte> payload , int count , out RtcpByeMessage result )
        {
            throw new NotImplementedException();
        }
    }
}
