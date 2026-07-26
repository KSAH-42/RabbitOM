using System;
using System.IO;

namespace RabbitOM.Streaming.RtspV2
{
    using RabbitOM.Streaming.RtspV2.Headers;

    // TODO: rename this class to avoid confusion with the client.Options method 
    public sealed class RtspClientRequestInfo
    {
        public Uri Uri { get; }

        public RequestsRtspHeaderCollection Headers { get; }

        public Stream Body { get; }
    }
}
