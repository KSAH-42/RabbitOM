using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{    
    public sealed class NetworkRtspStream : RtspStream
    {
        private int _position = 0;
        private int _length = 0;
        private readonly byte[] _buffer;
        private readonly Stream _innerStream;

        


        public NetworkRtspStream(Stream innerStream, int bufferSize )
        {
            if ( innerStream == null )
            {
                throw new ArgumentNullException( nameof( innerStream ) );
            }

            if ( bufferSize <= 0 )
            {
                throw new ArgumentException( nameof( bufferSize ) );
            }

            _innerStream = innerStream;

            _buffer = new byte[ bufferSize ];
        }





        public override int BufferingSize
        {
            get => throw new NotImplementedException();
        }

        public override bool CanRead
        {
            get => throw new NotImplementedException();
        }

        public override bool CanSeek
        {
            get => false;
        }

        public override bool CanWrite
        {
            get => throw new NotImplementedException();
        }

        public override long Length
        {
            get => _length; // throw new NotSupportedException();
        }

        public override long Position 
        { 
            get => _position; // throw new NotSupportedException(); 
            set => throw new NotSupportedException(); 
        }

        public override int PeekByte()
        {
            EnsureNotDisposed();

            throw new NotImplementedException(); 
        }

        public override string ReadLine()
        {
            EnsureNotDisposed();

            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            EnsureNotDisposed();

            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            EnsureNotDisposed();

            _innerStream.Write( buffer , offset , count );
        }

        public override void Flush()
        {
            EnsureNotDisposed();

            _innerStream.Flush();
        }
        
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }
        
        
        
        




        private void EnsureNotDisposed()
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if ( disposing )
            {
                _innerStream.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
