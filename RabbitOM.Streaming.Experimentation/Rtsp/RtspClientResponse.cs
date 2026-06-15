using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspClientResponse
    {
        public RtspClientResponse( RtspStatusCode statusCode , string reason )
        {
            StatusCode = statusCode;
            Reason = reason ?? string.Empty;
        }




        public bool IsSuccessStatusCode { get => StatusCode >= (RtspStatusCode) 200 && StatusCode <= (RtspStatusCode) 299; }

        public RtspStatusCode StatusCode { get; }

        public string Reason { get; }

        public ResponsesRtspHeaderCollection Headers { get; } = new ResponsesRtspHeaderCollection();

        public Stream Body { get; } = new MemoryStream();





        public void EnsureSuccess()
        {
            if ( ! IsSuccessStatusCode )
            {
                throw new InvalidOperationException();
            }
        }
    }
}
