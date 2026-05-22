using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    internal sealed class RtspMessageReader
    {
        private readonly Stream _stream;
        
        public RtspMessageReader( Stream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public RtspMessage ReadElement( char prefixValue )
        {
            var startLine = _stream.ReadLine();

            if ( startLine == null )
            {
                return null;
            }

            var message = new RtspMessage()
            {
                StartLine = $"{prefixValue }{startLine}"
            };

            while ( true )
            {
                var line = _stream.ReadLine();

                if ( line == null )
                {
                    return null;
                }

                if ( line == string.Empty )
                {
                    break;
                }

                if ( RtspMessageHeader.TryParse( line , out var header ) )
                {
                    message.Headers.Add( header );
                }
            }

            var contentLength = RtspMessage.GetContentLength( message );

            if ( contentLength > 0 )
            {
                message.Body = new byte[ contentLength ];

                var offset = 0;

                while ( offset < message.Body.Length )
                {
                    var bytesRead = _stream.Read( message.Body , offset , message.Body.Length - offset );

                    if ( bytesRead <= 0 )
                    {
                        return null;
                    }

                    offset += bytesRead;
                }
            }

            return message;
        }
    }
}
