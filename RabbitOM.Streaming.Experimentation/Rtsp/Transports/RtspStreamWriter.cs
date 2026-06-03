using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class RtspStreamWriter
    {
        private readonly IStream _stream;

        public RtspStreamWriter( IStream stream )
        {
            _stream = stream ?? throw new NotImplementedException( nameof( stream ) );
        }

        public void Write( byte[] buffer , int offset , int count )
        {
            throw new NotImplementedException();
        }

        public void WriteChar( char value )
        {
            throw new NotImplementedException();
        }

        public void WriteByte( byte value )
        {
            throw new NotImplementedException();
        }

        public void WriteLine()
        {
            throw new NotImplementedException();
        }

        public void WriteLine( string line )
        {
            throw new NotImplementedException();
        }

        public void WriteLine( params object[] args )
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }
    }
}
