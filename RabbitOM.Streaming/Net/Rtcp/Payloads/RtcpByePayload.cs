using System;

namespace RabbitOM.Streaming.Net.Rtcp.Payloads
{
    public struct RtcpByePayload
    {
        public uint[] SynchronizationSourcesIds { get; private set; }
        
        public string Reason { get; private set; }

        public static bool TryParse( in ArraySegment<byte> payload , int count , out RtcpByePayload result )
        {
            throw new NotImplementedException();
        }
    }
}
