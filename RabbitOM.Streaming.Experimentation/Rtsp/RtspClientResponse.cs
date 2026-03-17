using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.HeadersToBeRemoved;

    public sealed class RtspClientResponse : IDisposable
    {
        public RtspMethod Method { get; }
        
        public Version Version { get; }
        
        public RtspStatusCode Status { get; }
        
        public string ReasonPhrase { get; }
        
        public IReadOnlyHeaderCollection Headers { get; }
        





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

        public int GetContentLength()
        {
            throw new NotImplementedException();
        }

        public byte[] GetContent()
        {
            throw new NotImplementedException();
        }

        public string GetContentAsString()
        {
            throw new NotImplementedException();
        }
    }
}
