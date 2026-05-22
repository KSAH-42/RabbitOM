using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspRequestMessage
    {        
        public string Method { get; set; }
        public string Uri { get; set; }
        public string Protocol { get; set; }
        public string Version { get; set; }
        public ICollection<RtspMessageHeader> Headers { get; set; }
        public byte[] Body { get; set; }
    }
}
