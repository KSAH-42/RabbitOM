using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    // we don't use PipeReader class, instead we used buffered mecanism
    // we grab using a large buffer, and read the content until
    // touching the limit, and then we trigger a new capture of incomming data
    // and continue to read even if it's an incomplete receive
    // using this approach readByte become fast and we can introduce peek method

    // TODO: renaming as NetworkRtspStream
    public sealed class RtspStream : IStream
    {
        private readonly ITransport _transport;
        private readonly MemoryStream _outputStream;
        private readonly byte[] _writeBuffer;
        private readonly byte[] _readBuffer;
        private int _readPosition;
        private int _readRemainingBytes;




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
            _outputStream = new MemoryStream();
            _writeBuffer = new byte[ writeBufferSize ];
            _readBuffer = new byte[ readBufferSize ];
        }






        public int Peek()
        {
            EnsureBuffering();

            // TODO: /!\ why using this statement here ?
            // we are ensuring something before !
            // this is bad, remove this code AND
            // make somewhere a refactoring inside this class
            // and never put this code in production
            if ( _readRemainingBytes <= 0 || _readPosition < 0 || _readPosition >= _readBuffer.Length )
            {
                return -1;
            }

            return _readBuffer[ _readPosition ];
        }

        public int ReadByte()
        {
            EnsureBuffering();

            if ( _readRemainingBytes <= 0 || _readPosition < 0 || _readPosition >= _readBuffer.Length )
            {
                return -1;
            }

            _readRemainingBytes --;

            return _readBuffer[ _readPosition ++ ];
        }

        public int Read( byte[] buffer , int offset , int count )
        {
            throw new NotImplementedException();
        }

        public void WriteByte( byte value )
        {
            _outputStream.WriteByte( value );
        }

        public void Write( byte[] buffer , int offset , int count )
        {
            _outputStream.Write( buffer , offset , count );
        }

        public void Flush()
        {
            try
            {
                var offset = 0;

                while ( offset < _readBuffer.Length )
                {
                    var bytesRead = _outputStream.Read( _readBuffer , offset , _readBuffer.Length );

                    if ( bytesRead <= 0 )
                    {
                        break;
                    }

                    _transport.Send( _writeBuffer , offset , bytesRead );

                    offset += bytesRead;
                }
            }
            finally
            {
                _outputStream.Seek( 0 , SeekOrigin.Begin );
            }
        }

        public void Close()
        {
            _outputStream.Close();
            _transport.Close();
        }

        // TODO: adding a parameter on the ctor to tell if dispose method must be called or just detach it (set as null)

        public void Dispose()
        {
            _outputStream.Dispose();
            _transport.Dispose();
        }

        private void EnsureBuffering()
        {
            if ( _readRemainingBytes <= 0 )
            {
                _readPosition = 0;
                _readRemainingBytes = _transport.Receive( _readBuffer , 0 , _readBuffer.Length );
            }
        }
    }
}