using System;

namespace RabbitOM.Streaming.Net.Rtcp
{
    public sealed class RtcpSourceDescriptionItem
    {
        public byte Type { get; private set; }
        
        public string Value { get; private set; }




        public bool TryParse( in ArraySegment<byte> payload , out RtcpSourceDescriptionItem result )
        {
            throw new NotImplementedException();
        }
    }
}
