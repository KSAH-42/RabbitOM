using System;
using System.IO;

namespace RabbitOM.Streaming.RtspV2.Transports
{
    public sealed class RtspStreamWriter
    {
        private readonly IStream _stream;

        public RtspStreamWriter( IStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
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
            foreach ( var element in string.Format( format , args ) )
            {
                _stream.WriteByte( (byte) element );
            }

            _stream.WriteByte( (byte) '\r' );
            _stream.WriteByte( (byte) '\n' );
        }

        public void Write( byte[] buffer )
        {
            if ( buffer == null || buffer.Length <= 0 )
            {
                return;
            }

            _stream.Write( buffer , 0 , buffer.Length );
        }

        public void Write( byte[] buffer , int offset , int count )
        {
            if ( buffer == null || buffer.Length <= 0 || offset < 0 || count < 0 )
            {
                return;
            }

            if ( buffer.Length - offset < count )
            {
                return;
            }

            _stream.Write( buffer , offset , count );
        }

        public void Write( Stream stream )
        {
            if ( stream == null || stream.Length <= 0 )
            {
                return;
            }

            var buffer = new byte[ 1024 ];

            while ( true )
            {
                var bytesRead = stream.Read( buffer , 0 , buffer.Length );

                if ( bytesRead <= 0 )
                {
                    break;
                }

                _stream.Write( buffer , 0 , bytesRead );
            }
        }

        public void Flush()
        {
            _stream.Flush();
        }
    }
}
