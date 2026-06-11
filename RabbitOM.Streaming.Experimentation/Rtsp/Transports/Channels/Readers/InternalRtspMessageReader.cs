using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    internal sealed class InternalRtspMessageReader
    {
        public const int DefaultMaximumOfHeaders = 100;
        public const int DefaultMaximumOfHeaderSize = 8192;

        private readonly RtspStreamReader _reader;

        public InternalRtspMessageReader( IStream stream )
        {
            _reader = new RtspStreamReader( stream );
        }

        public long? MaximumOfHeaders { get; set; }

        public long? MaximumOfHeadersSize { get; set; }

        public int PeekValue()
        {
            return _reader.Peek();
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

            var length = (ushort) ( (lengthMsb << 8  + lengthLsb) & 0xFFFF );

            if ( length <= 0 )
            {
                return null;
            }

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

        public RtspMessage ReadMessage()
        {
            var startLine = _reader.ReadLine();

            if ( startLine == null )
            {
                return null;
            }

            var headers = new RtspHeaderCollection();

            var verifier = new RtspHeaderCollectionVerifier( headers )
            {
                MaximumOfHeaders = MaximumOfHeaders,
                MaximumOfHeadersSize = MaximumOfHeadersSize,
            };

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

                if ( headers.TryAddParse( header ) )
                {
                    verifier.IncreaseTotalSize( header.Length );
                    verifier.EnsureNoIllegalDuplication();
                    verifier.EnsureMaximumOfHeaders();
                }
            }

            byte[] body = null;

            if ( headers.ContentLength.HasValue && headers.ContentLength > 0 )
            {
                body = new byte[ headers.ContentLength.Value ]; // TODO: use allocator

                var offset = 0;

                while ( offset < body.Length )
                {
                    var bytesRead = _reader.Read( body , offset , body.Length - offset );

                    if ( bytesRead <= 0 )
                    {
                        break;
                    }

                    offset += bytesRead;
                }
            }

            // Response from the server ? Most of the times... 
            if ( RtspStatusLine.TryParse( startLine , out var statusLine ) )
            {
                return new RtspResponseMessage() { StatusLine = statusLine , Headers = headers , Body = body };
            }

            // Request from the server ? (see rfc) in rare cases
            if ( RtspRequestLine.TryParse( startLine , out var requestLine ) )
            {
                return new RtspRequestMessage() { RequestLine = requestLine , Headers = headers , Body = body };
            }

            return null;
        }
    }
}
