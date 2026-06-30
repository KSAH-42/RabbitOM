using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public sealed class RtspContext
    {
        public RtspContext( string method , string uri )
        {
            Method = method;
            Uri = uri;
        }

        public string Method { get; }

        public string Uri { get; }
    }
}
