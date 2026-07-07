using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspClientRequestOptions
    {
        public Uri Uri { get; }

        public RequestsRtspHeaderCollection Headers { get; }

        public Stream Body { get; }
    }
}
