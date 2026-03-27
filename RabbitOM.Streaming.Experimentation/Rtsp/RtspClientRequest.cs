using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspClientRequest
    {
        public HeaderCollectionRequest Headers { get; } = new HeaderCollectionRequest();
        public MemoryStream Body { get; } = new MemoryStream();
    }
}
