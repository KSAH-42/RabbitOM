using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspMessageReader : IMessageReader
    {
        private const int ContentBufferSize = 512;





        private readonly RtspStreamReader _reader;

        private readonly RtspMessageReaderValidatorOptions _options;





        public RtspMessageReader( IStream stream , RtspMessageReaderValidatorOptions options )
        {
            _reader = new RtspStreamReader( stream );
            _options = options ?? throw new ArgumentNullException( nameof( options ) );
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

            var headers = new RtspMessageHeaderCollection();
            var validator = new RtspMessageReaderValidator( headers , _options );

            while ( true )
            {
                var header = _reader.ReadLine( _options.MaximumOfHeaderLength );

                if ( header == null )
                {
                    return null;
                }

                if ( header == "" )
                {
                    break;
                }

                validator.Validate( header );

                headers.TryAddParse( header );
            }

            validator.Validate();

            var body = new MemoryStream();

            var contentLength = headers.ContentLength;

            if ( contentLength.HasValue && contentLength > 0 )
            {
                var buffer = new byte[ ContentBufferSize ]; // don't move as private member because most of the time rtsp bodies are unused, just lets the gc collect this buffer, instead to have this one always present all the time

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


