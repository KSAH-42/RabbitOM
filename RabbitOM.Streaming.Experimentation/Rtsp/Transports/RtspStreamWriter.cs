using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class RtspStreamWriter
    {
        private readonly IStream _stream;

        public RtspStreamWriter( IStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public void Write( byte[] buffer , int offset , int count )
        {
            _stream.Write( buffer , offset , count );
        }

        public void Write( Stream stream )
        {
            _stream.Write( stream );
        }

        public void WriteChar( in char value )
        {
            _stream.WriteByte( (byte) value );
        }

        public void WriteByte( in byte value )
        {
            _stream.WriteByte( value );
        }

        public void WriteLine()
        {
            _stream.WriteByte( (byte) '\r' );
            _stream.WriteByte( (byte) '\n' );
        }

        public void WriteLine( string line )
        {
            if ( line == null )
            {
                return;
            }

            foreach ( var element in line )
            {
                _stream.WriteByte( (byte) element );
            }

            _stream.WriteByte( (byte) '\r' );
            _stream.WriteByte( (byte) '\n' );
        }

        public void WriteLine( string format , params object[] args )
        {
            var line = string.Format( format , args );

            foreach ( var element in line )
            {
                _stream.WriteByte( (byte) element );
            }

            _stream.WriteByte( (byte) '\r' );
            _stream.WriteByte( (byte) '\n' );
        }

        public void Flush()
        {
            _stream.Flush();
        }
    }
}
