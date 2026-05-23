using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging
{
    public sealed class RtspResponseMessage : RtspMessage
    {
        public string Protocol { get; set; }
        public string Version { get; set; }
        public string Code { get; set; }
        public string Reason { get; set; }
        public List<RtspHeader> Headers { get; set; }
        public byte[] Body { get; set; }
    }
}
