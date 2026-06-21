using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Tests.Rtsp.Transports
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Transports;

    public sealed class MemoryRtspTransport : ITransport
    {
        private readonly MemoryStream _stream;

        public MemoryRtspTransport( MemoryStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public void Close()
        {
            _stream.Close();
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        public int Receive( byte[] buffer , int offset , int count )
        {
            return _stream.Read( buffer , offset , count );
        }

        public void Send( byte[] buffer , int offset , int count )
        {
            // just do nothing
        }
    }
}
