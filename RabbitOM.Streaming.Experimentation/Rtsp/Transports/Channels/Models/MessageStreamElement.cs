using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Models
{
    public sealed class MessageStreamElement : IStreamElement
    {
        public List<string> Metadata { get; } = new List<string>();
        public byte[] Body { get; set; }

        public static int GetContentLength( MessageStreamElement message )
        {
            throw new NotImplementedException();
        }
    }
}
