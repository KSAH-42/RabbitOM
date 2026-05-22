using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    public sealed class RtspMessageReader : IStreamReader
    {
        private readonly Stream _stream;

        public RtspMessageReader( Stream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public IStreamElement ReadElement()
        {
            var message = new RtspMessage()
            {
                StartLine = _stream.ReadLine()
            };

            if ( message.StartLine == null )
            {
                return null;
            }

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
