using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class RtspStream : IStream
    {
        private readonly ITransport _transport;
        private readonly int _bufferSize;
        private readonly MemoryStream _readStream;
        private readonly MemoryStream _writeStream;


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
            _bufferSize = bufferSize;
        }



        public bool CanRead => true;

        public bool CanWrite => true;



        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public int PeekByte()
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

        public int Read( byte[] buffer , int offset , int count )
        {
            throw new NotImplementedException();
        }

        public void Write( byte[] buffer , int offset , int count )
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

        public void WriteLine( string value )
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        private bool EnsureCachingData()
        {
            // if ( _position >= _buffer.Length ) 
            //    _transport.Receive( _buffer , 0 , _buffer.length );

            throw new NotImplementedException();
        }
    }
}
