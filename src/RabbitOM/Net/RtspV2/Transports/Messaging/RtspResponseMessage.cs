using System;
using System.IO;

namespace RabbitOM.Net.RtspV2.Transports
{
    public sealed class RtspResponseMessage : RtspMessage
    {
        public RtspStatusLine StatusLine { get; set; }

        public RtspMessageHeaderCollection Headers { get; set; }

        public Stream Body { get; set; }
    }
}
