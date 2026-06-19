using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspClientRequest
    {
        public RtspClientRequest()
            : this ( new RequestsRtspHeaderCollection() , new MemoryStream() )
        {
        }

        public RtspClientRequest( RequestsRtspHeaderCollection headers )
            : this ( headers , new MemoryStream() )
        {
        }

        public RtspClientRequest( RequestsRtspHeaderCollection headers , Stream body )
        {
            Headers = headers ?? throw new ArgumentNullException( nameof( headers ) );
            Body = body ?? throw new ArgumentNullException( nameof( body ) );
        }

        public RequestsRtspHeaderCollection Headers { get; }

        public Stream Body { get; }
    }
}
