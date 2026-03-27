using System;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Transports
{
    public sealed class RtspRequestMessage
    {        
        public string Method { get; set; }

        public string Version { get; set; }

        public string Uri { get; set; }

        public RtspMessageHeader[] Headers { get; set; }

        public byte[] Body { get; set; }
    }
}
