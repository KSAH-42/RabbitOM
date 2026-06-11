using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspResponseMessage : RtspMessage
    {
        public RtspStatusLine StatusLine { get; set; }

        public RtspMessageHeaderCollection Headers { get; set; }

        public byte[] Body { get; set; }
    }
}
