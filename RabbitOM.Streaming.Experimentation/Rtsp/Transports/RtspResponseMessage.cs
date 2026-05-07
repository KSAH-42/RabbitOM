using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class RtspResponseMessage
    {        
        public string Method { get; set; }
        
        public string Version { get; set; }
        
        public string ReasonPhase { get; set; }

        public int StatusCode { get; set; }

        public ICollection<RtspMessageHeader> Headers { get; set; }

        public ArraySegment<byte> Body { get; set; }
    }
}
