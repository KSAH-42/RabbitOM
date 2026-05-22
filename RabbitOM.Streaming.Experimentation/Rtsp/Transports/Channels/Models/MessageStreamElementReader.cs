using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Models
{
    public sealed class MessageStreamElementReader : IStreamElementReader
    {
        private readonly Stream _stream;
       
        public MessageStreamElementReader( Stream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public IStreamElement ReadElement()
        {
            var message = new MessageStreamElement();

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

                message.Metadata.Add( line );
            }

            var contentLength = MessageStreamElement.GetContentLength( message );

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
