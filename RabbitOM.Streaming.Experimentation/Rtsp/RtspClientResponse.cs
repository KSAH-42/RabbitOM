using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
    
    public sealed class RtspClientResponse 
    {
        public RtspStatusCode Status { get; }
        
        public ResponsesRtspHeaderCollection Headers { get; }
        
        public Stream Body { get; }

        public void EnsureSuccess()
        {
            throw new NotImplementedException();
        }
    }
}
