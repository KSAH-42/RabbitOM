using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspRequestResponseMessageReader : IMessageReader
    {
        private readonly IStream _stream;

        public RtspRequestResponseMessageReader( IStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public int? MaximumOfHeaders { get; set; } // For untrusted source

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

                if ( MaximumOfHeaders.HasValue && MaximumOfHeaders.Value > headers.Count )
                {
                    // do not continue to read, just stop or avoid to make something tolerant
                    // that's an anormal situation and the execution flow should be interrupted,
                    // just stop and close the communication and even retry later
                    // and do not await the content-length undefinetively and extract the body in order to make it a tolerant read, it's a defect somewhere
                    // throw exception now
                    throw new System.Net.ProtocolViolationException( $"too many headers:{headers.Count}. That's an anormal situation to many headers in one single message, it seems that we received malformed packets, communication must be closed" );
                }

                headers.TryAddParse( header );
            }

            byte[] body = null;

            if ( headers.ContentLength.HasValue && headers.ContentLength > 0 )
            {
                body = new byte[ headers.ContentLength.Value ]; // TODO: use allocator

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
