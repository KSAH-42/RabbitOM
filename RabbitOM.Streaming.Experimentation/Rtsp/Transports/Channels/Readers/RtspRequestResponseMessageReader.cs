using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspRequestResponseMessageReader
    {
        private readonly IStream _stream;

        public RtspRequestResponseMessageReader( IStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public RtspMessage ReadMessage()
        {
            var startLine = _stream.ReadLine();

            if ( startLine == null )
            {
                return null;
            }

            var headers = new RtspHeaderCollection();

            while ( true )
            {
                var header = _stream.ReadLine();

                if ( header == null )
                {
                    return null;
                }

                if ( header == "" )
                {
                    break;
                }

                headers.TryAddParse( header );
            }

            byte[] body = null;

            if ( headers.ContentLength.HasValue && headers.ContentLength > 0 )
            {
                body = new byte[ headers.ContentLength.Value ];

                var offset = 0;

                while ( offset < body.Length )
                {
                    var bytesRead = _stream.Read( body , offset , body.Length - offset );

                    if ( bytesRead <= 0 )
                    {
                        break;
                    }

                    offset += bytesRead;
                }
            }

            // Response from the server ?
            if ( RtspStatusLine.TryParse( startLine , out var statusLine ) )
            {
                return new RtspResponseMessage() { StatusLine = statusLine , Headers = headers , Body = body };
            }

            // Request from the server ? (see rfc)
            if ( RtspRequestLine.TryParse( startLine , out var requestLine ) )
            {
                return new RtspRequestMessage() { RequestLine = requestLine , Headers = headers , Body = body };
            }

            return null;
        }
    }
}
