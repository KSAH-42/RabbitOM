using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class RtspRequestMessage
    {        
        public string Method { get; set; }

        public string Version { get; set; }

        public string Uri { get; set; }

        public ICollection<RtspMessageHeader> Headers { get; set; }

        public ArraySegment<byte> Body { get; set; }
    }
}
