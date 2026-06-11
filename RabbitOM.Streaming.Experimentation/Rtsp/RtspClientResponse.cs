using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspClientResponse
    {
        public RtspClientResponse( RtspStatusCode status , string reason )
        {
            Status = status;
            Reason = reason ?? string.Empty;
        }





        public RtspStatusCode Status { get; }

        public string Reason { get; }

        public ResponsesRtspHeaderCollection Headers { get; } = new ResponsesRtspHeaderCollection();

        public Stream Body { get; } = new MemoryStream();







        public void EnsureSuccess()
        {
            if ( Status == RtspStatusCode.OK )
            {
                return;
            }

            throw new InvalidOperationException();
        }
    }
}
