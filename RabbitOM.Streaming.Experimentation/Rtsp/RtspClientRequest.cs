using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspClientRequest
    {
        public RequestHeaderCollection Headers { get; } = new RequestHeaderCollection();
        public MemoryStream Body { get; } = new MemoryStream();
    }
}
