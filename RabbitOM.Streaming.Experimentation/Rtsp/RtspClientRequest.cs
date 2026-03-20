using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspClientRequest
    {
        public RtspRequestHeaderCollection Headers { get; } = new RtspRequestHeaderCollection();
        public MemoryStream Body { get; } = new MemoryStream();
    }
}
