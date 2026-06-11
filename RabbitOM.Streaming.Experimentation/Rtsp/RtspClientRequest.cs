using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspClientRequest
    {
        public RequestsRtspHeaderCollection Headers { get; } = new RequestsRtspHeaderCollection();

        public Stream Body { get; } = new MemoryStream();
    }
}
