using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspClientRequest
    {
        public RtspClientRequest()
        {
            Headers = new RequestsRtspHeaderCollection();
        }

        public RtspClientRequest( RequestsRtspHeaderCollection headers )
        {
            Headers = headers ?? throw new ArgumentNullException( nameof( headers ) );
        }

        public RequestsRtspHeaderCollection Headers { get; }

        public MemoryStream Body { get; } = new MemoryStream();
    }
}
