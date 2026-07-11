using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspMessageReader : IMessageReader
    {
        private const int ContentBufferSize = 512;





        private readonly RtspStreamReader _reader;

        private readonly IMessageReaderValidator _validator;





        public RtspMessageReader( IStream stream , IMessageReaderValidator validator )
        {
            _validator = validator ?? throw new ArgumentNullException( nameof( validator ) );

            _reader = new RtspStreamReader( stream );
        }




        public byte? PeekValue()
        {
            var prefix = _reader.Peek();

            if ( prefix < 0 )
            {
                return null;
            }

            return (byte) prefix;
        }

        public RtspMessage ReadControlMessage()
        {
            var startLine = _reader.ReadLine();

            if ( startLine == null )
            {
                return null;
            }

            _validator.Setup();

            var headers = new RtspMessageHeaderCollection();

            while ( true )
            {
                var header = _reader.ReadLine();

                if ( header == null )
                {
                    return null;
                }

                if ( header == "" )
                {
                    break;
                }

                _validator.Validate( headers , header );

                headers.TryAddParse( header );
            }

            _validator.Validate( headers );

            var body = new MemoryStream();

            var contentLength = headers.ContentLength;

            if ( contentLength.HasValue && contentLength > 0 )
            {
                var buffer = new byte[ ContentBufferSize ]; // don't move as private buffer because most of the time rtsp bodies are unused and lets the gc collect this buffer, instead to have this one always present and unsed and allocated each times the channel is created

                while ( body.Length < contentLength.Value )
                {
                    var bytesRead = _reader.Read( buffer , 0 , buffer.Length );

                    if ( bytesRead <= 0 )
                    {
                        return null;
                    }

                    body.Write( buffer , 0 , bytesRead );
                }

                body.Seek( 0 , SeekOrigin.Begin );
            }

            if ( RtspStatusLine.TryParse( startLine , out var statusLine ) )
            {
                return new RtspResponseMessage() { StatusLine = statusLine , Headers = headers , Body = body };
            }

            if ( RtspRequestLine.TryParse( startLine , out var requestLine ) )
            {
                return new RtspRequestMessage() { RequestLine = requestLine , Headers = headers , Body = body };
            }

            return null;
        }

        public RtspMessage ReadInterleavedMessage()
        {
            var magicByte = _reader.ReadByte();

            if ( magicByte != '$' )
            {
                return null;
            }

            var channel = _reader.ReadByte();

            if ( channel < 0 )
            {
                return null;
            }

            var lengthMsb = _reader.ReadByte();

            if ( lengthMsb < 0 )
            {
                return null;
            }

            var lengthLsb = _reader.ReadByte();

            if ( lengthLsb < 0 )
            {
                return null;
            }

            var length = (ushort) ( (lengthMsb << 8 + lengthLsb) & 0xFFFF );
            var buffer = new byte[ length ];
            var offset = 0;

            while ( offset < buffer.Length )
            {
                var bytesRead = _reader.Read( buffer , offset , buffer.Length - offset );

                if ( bytesRead <= 0 )
                {
                    return null;
                }

                offset += bytesRead;
            }

            return new RtspInterleavedMessage() { Channel = (byte) ( channel & 0xFF ) , Length = length , Buffer = buffer };
        }
    }
}


