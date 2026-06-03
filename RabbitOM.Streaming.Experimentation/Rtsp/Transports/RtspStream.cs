using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class RtspStream : IStream
    {
        private readonly ITransport _transport;
        private readonly MemoryStream _readCache;  // use a byte array instead ?
        private readonly MemoryStream _writeCache; // for flushing
        private readonly int _readBufferSize;      // the quantity of bytes to put on the read cache
        private readonly int _writeBufferSize;     // the quantity of bytes to send to the transport layer, we can control the size of output chunch





        public RtspStream( ITransport transport )
            : this ( transport , 1024 , 1024 )
        {
        }

        public RtspStream( ITransport transport , int readBufferSize , int writeBufferSize )
        {
            if ( transport == null )
            {
                throw new ArgumentNullException( nameof( transport ) );
            }

            if ( readBufferSize <= 0 )
            {
                throw new ArgumentException( nameof( readBufferSize ) );
            }

            if ( writeBufferSize <= 0 )
            {
                throw new ArgumentException( nameof( writeBufferSize ) );
            }

            _transport = transport;
            _readCache = new MemoryStream();
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

        public void Discard()
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
