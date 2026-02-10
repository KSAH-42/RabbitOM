using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.RtspV2
{
    public sealed class RtspResponseMessage : IDisposable
    {
        public RtspMethod Method { get; set; }
        public Version Version { get; set; }
        public string ReasonPhrase { get; set; }
        public RtspStatusCode Status { get; set; }
        public IReadOnlyDictionary<string,string> Headers { get; set; }
        public RtspResponseBody Body { get; set; }



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
