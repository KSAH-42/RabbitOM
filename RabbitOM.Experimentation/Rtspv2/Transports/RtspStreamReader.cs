using System;
using System.Text;

namespace RabbitOM.Streaming.RtspV2.Transports
{
    public sealed class RtspStreamReader
    {
        private readonly IStream _stream;
        private readonly int? _maximumOfHeaderLength;

        public RtspStreamReader( IStream stream , int? maximumOfHeaderLength = default )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );

            _maximumOfHeaderLength = maximumOfHeaderLength;
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

        public string ReadLine()
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

                if ( _maximumOfHeaderLength.HasValue && _maximumOfHeaderLength <= builder.Length )
                {
                    throw new InvalidOperationException( "the length is too big" );
                }

                builder.Append( (char) byteValue );
            }

            return builder.ToString();
        }
    }
}
