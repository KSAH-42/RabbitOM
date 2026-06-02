using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class RtspStream : IStream
    {
        private readonly ITransport _transport;
        private readonly MemoryStream _readCache; // use a byte array instead ?
        private readonly MemoryStream _writeCache; // for flushing



        public RtspStream( ITransport transport )
            : this ( transport , 8024 )
        {
        }

        public RtspStream( ITransport transport , int bufferSize )
        {
            if ( transport == null )
            {
                throw new ArgumentNullException( nameof( transport ) );
            }

            if ( bufferSize <= 0 )
            {
                throw new ArgumentException( nameof( bufferSize ) );
            }

            _transport = transport;
            _readCache = new MemoryStream( bufferSize );
            _writeCache = new MemoryStream();
        }







        public int ReadByte()
        {
            throw new NotImplementedException();
        }

        public int Read( byte[] buffer , int offset , int count )
        {
            throw new NotImplementedException();
        }

        public void WriteByte( byte value )
        {
            throw new NotImplementedException();
        }

        public void Write( byte[] buffer , int offset , int count )
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
