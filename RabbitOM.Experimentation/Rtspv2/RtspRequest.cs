using System;
using System.IO;

namespace RabbitOM.Streaming.RtspV2
{
    using RabbitOM.Streaming.RtspV2.Headers;

    public sealed class RtspRequest
    {
        public RtspMethod Method { get; }

        public string Uri { get; }

        public Version Version { get; }

        public RequestsRtspHeaderCollection Headers { get; }

        public Stream Body { get; }
    }
}
