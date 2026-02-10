using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public sealed class RtspResponseBody : IDisposable
    {
        public byte[] Read() => throw new NotImplementedException();
        public string ReadAsString() => throw new NotImplementedException();
        public Stream ReadAsStream() => throw new NotImplementedException();
        public void Dispose() => throw new NotImplementedException();
    }
}
