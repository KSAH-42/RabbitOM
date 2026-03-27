using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved
{
    using RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers;

    public sealed class RtspClientRequest
    {
        public RtspRequestHeaderCollection Headers { get; } = new RtspRequestHeaderCollection();
        public MemoryStream Body { get; } = new MemoryStream();
    }
}
