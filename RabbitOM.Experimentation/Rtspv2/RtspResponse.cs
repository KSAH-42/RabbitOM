using System;
using System.IO;

namespace RabbitOM.Streaming.RtspV2
{
    using RabbitOM.Streaming.RtspV2.Headers;

    public sealed class RtspResponse
    {
        public Version Version { get; }

        public RtspStatusCode StatusCode { get; }

        public string Reason { get; }

        public ResponsesRtspHeaderCollection Headers { get; }

        public Stream Body { get; }

        public RtspRequest Request { get; }





        public bool IsSuccessStatusCode()
        {
            var code = (int) StatusCode;

            return 200 <= code && code <= 299;
        }

        public RtspResponse EnsureSuccess()
        {
            return IsSuccessStatusCode() ? this : throw new InvalidOperationException();
        }
    }
}
