using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging
{
    public sealed class RtspRequestMessage : RtspMessage
    {
        public string Method { get; set; }
        public string Uri { get; set; }
        public string Protocol { get; set; }
        public string Version { get; set; }
        public List<RtspHeader> Headers { get; set; }
        public byte[] Body { get; set; }


        internal static bool IsRequestMessage( string startLine )
        {
            throw new NotImplementedException();
        }
    }
}
