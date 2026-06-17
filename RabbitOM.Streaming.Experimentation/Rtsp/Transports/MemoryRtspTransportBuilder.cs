using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    // Temp class for testing that will be moved to a testing assembly
    internal sealed class MemoryRtspTransportBuilder
    {
        private readonly MemoryStream _stream = new MemoryStream();

        public MemoryRtspTransportBuilder WriteStatus( string text )
        {
            if ( text != null )
            {
                var buffer = System.Text.Encoding.UTF8.GetBytes( text + "\r\n" );

                _stream.Write( buffer , 0 , buffer.Length );
            }
            
            return this;
        }

        public MemoryRtspTransportBuilder WriteHeader( string name , string value )
        {
            if ( name != null )
            {
                var header = string.Format( "{0}: {1}\r\n" , name , value ?? string.Empty );

                var buffer = System.Text.Encoding.UTF8.GetBytes( header );

                _stream.Write( buffer , 0 , buffer.Length );
            }
            
            return this;
        }

        public MemoryRtspTransportBuilder WriteLine()
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes( "\r\n" );

            _stream.Write( buffer , 0 , buffer.Length );
            
            return this;
        }

        public MemoryRtspTransportBuilder WriteInterleaved( byte channel , byte[] payload )
        {
            if ( payload != null && payload.Length > 0 )
            {
                _stream.WriteByte( (byte) '$' );
                _stream.WriteByte( channel );
                _stream.WriteByte( (byte) (payload.Length >> 8 & 0xFF) );
                _stream.WriteByte( (byte) (payload.Length      & 0xFF) );
                _stream.Write( payload , 0 , payload.Length & 0xFFFF );
            }

            return this;
        }

        public MemoryRtspTransport Build()
        {
            var result = new MemoryRtspTransport( new MemoryStream( _stream.ToArray() ) );

            _stream.Seek( 0 , SeekOrigin.Begin );

            return result;

        }
    }
}
