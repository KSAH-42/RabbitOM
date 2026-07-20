using System;
using System.IO;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels
{
    public sealed class RtspResponseMessage : RtspMessage
    {
        public RtspStatusLine StatusLine { get; set; }

        public RtspMessageHeaderCollection Headers { get; set; }

        public Stream Body { get; set; }
    }
}
