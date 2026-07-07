using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspClientResponse
    {
        public bool IsSuccessStatusCode { get => RtspStatusCodeChecker.IsSuccessStatusCode( StatusCode ); }

        public RtspStatusCode StatusCode { get; }

        public string Reason { get; }

        public ResponsesRtspHeaderCollection Headers { get; }

        public Stream Body { get; } = new MemoryStream();

        public RtspClientRequest Request { get; }





        public RtspClientResponse EnsureSuccess()
        {
            if ( ! RtspStatusCodeChecker.IsSuccessStatusCode( StatusCode ) )
            {
                throw new InvalidOperationException();
            }

            return this;
        }
    }
}
