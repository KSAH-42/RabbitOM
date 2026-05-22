using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging
{
    public sealed class RtspMessage : IStreamElement
    {
        public string StartLine { get; set; }

        public List<RtspMessageHeader> Headers { get; set; }

        public byte[] Body { get; set; }



        public static int GetContentLength( RtspMessage message )
        {
            throw new NotImplementedException();
        }
    }
}
