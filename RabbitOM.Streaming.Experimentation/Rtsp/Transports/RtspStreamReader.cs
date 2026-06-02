using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class RtspStreamReader
    {
        private readonly IStream _stream;

        public RtspStreamReader( IStream stream )
        {
            _stream = stream ?? throw new NotImplementedException( nameof( stream ) );
        }

        public int Read( byte[] buffer , int offset , int count )
        {
            throw new NotImplementedException();
        }

        public int ReadByte()
        {
            throw new NotImplementedException();
        }

        public string ReadLine()
        {
            throw new NotImplementedException();
        }
    }
}
