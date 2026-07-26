using System;
using System.IO;

namespace RabbitOM.Streaming.RtspV2
{
    using RabbitOM.Streaming.RtspV2.Headers;

    public sealed class RtspClientRequestInfo
    {
        public Uri Uri { get; }

        public RequestsRtspHeaderCollection Headers { get; }

        public Stream Body { get; }
    }
}
