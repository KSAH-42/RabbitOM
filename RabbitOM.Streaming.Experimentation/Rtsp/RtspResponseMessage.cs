using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public sealed class RtspResponseMessage : IDisposable
    {
        public RtspMethod Method { get; }
        public Version Version { get; }
        public RtspStatusCode Status { get; }
        public string ReasonPhrase { get; }
        public IReadOnlyDictionary<string,string> Headers { get; }
        public RtspResponseBody Body { get; }



        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void IsSuccess()
        {
            throw new NotImplementedException();
        }

        public void EnsureSuccess()
        {
            throw new NotImplementedException();
        }
    }
}
