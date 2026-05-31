using System;
using System.Net.Sockets;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class RtspStream : IStream
    {
        private readonly ITransport _transport;
        private readonly byte[] _buffer;


        public RtspStream( ITransport transport )
            : this ( transport , 8024 )
        {
        }

        public RtspStream( ITransport transport , ushort bufferSize )
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
            _buffer = new byte[ bufferSize ];
        }

        ~RtspStream()
        {
            Dispose();
        }


        public bool CanRead => true;

        public bool CanWrite => true;



        public void Close()
        {
            _transport.Close();
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize( this );
        }

        public int PeekByte()
        {
            // EnsureCachingData();
            // return _buffer[ _position ];

            throw new NotImplementedException();
        }

        public int ReadByte()
        {
            // EnsureCachingData();
            // return _buffer[ ++ _position ];

            throw new NotImplementedException();
        }

        public string ReadLine()
        {
            // while ( true )
            //   >> EnsureCachingData();
            //     >> builder.Append( _buffer[ _position ])
            //     >> if ( _buffer[ ++ _position ] == '\r' )
            //       >> return builder.ToString()
            throw new NotImplementedException();
        }

        public int Read( byte[] buffer , int offset , int count )
        {
            // EnsureCachingData();
            // Array.Copy( _buffer , buffer , 0 , count );
            throw new NotImplementedException();
        }

        public void Write( byte[] buffer , int offset , int count )
        {
            _transport.Send( buffer , offset , count );
        }

        private bool EnsureCachingData()
        {
            // if ( _position >= _buffer.Length ) 
            //    _transport.Receive( _buffer , 0 , _buffer.length );

            throw new NotImplementedException();
        }
    }
}
