using System;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    public sealed class RtspStreamReader
    {
        private readonly IStream _stream;

        public RtspStreamReader( IStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public int Read( byte[] buffer , int offset , int count )
        {
            return _stream.Read( buffer , offset , count );
        }

        public int Peek()
        {
            return _stream.Peek();
        }

        public int ReadByte()
        {
            return _stream.ReadByte();
        }

        // TODO: /!\ security refactoring here
        public string ReadLine( /* int? headerLimit = null OR inject this parameter into the ctor */ )
        {
            var builder = new StringBuilder();

            while ( true )
            {
                var byteValue = _stream.ReadByte();

                if ( byteValue <= 0 )
                {
                    return null;
                }

                if ( byteValue == '\r' )
                {
                    continue;
                }

                if ( byteValue == '\n' )
                {
                    break;
                }

                builder.Append( (char) byteValue );
            }

            return builder.ToString();
        }
    }
}
