using System;
using System.ComponentModel.DataAnnotations;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspRequestMessage : RtspMessage
    {
        public RtspRequestLine RequestLine { get; set; }

        public RtspHeaderCollection Headers { get; set; }

        public byte[] Body { get; set; }
    }
}
