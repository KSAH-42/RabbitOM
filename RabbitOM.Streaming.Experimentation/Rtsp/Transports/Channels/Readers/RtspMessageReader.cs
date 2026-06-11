using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspMessageReader : IMessageReader
    {
        private readonly RtspStreamReader _reader;





        public RtspMessageReader( IStream stream )
        {
            _reader = new RtspStreamReader( stream );
        }





        public long? LimitOfHeadersCount { get; set; }

        public long? LimitOfCumulatedHeadersSize { get; set; }

        public long? LimitOfContentLength { get; set; }







        public byte? Peek()
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

            var headers = new RtspHeaderCollection();

            var validator = new RtspHeaderCollectionValidator( headers ) // for protocol violations
            {
                LimitOfHeadersCount = LimitOfHeadersCount,
                LimitOfCumulatedHeadersSize = LimitOfCumulatedHeadersSize,
                LimitOfContentLength = LimitOfContentLength,
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
                    validator.Validate( header );
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

        public RtspInterleavedMessage ReadInterleavedMessage()
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
    }
}
