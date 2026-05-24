using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    public sealed class RtspCommunicationReader : IMessageReader
    {
        private readonly Stream _stream;
        
        public RtspCommunicationReader( Stream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public RtspMessage ReadMessage()
        {
            throw new NotImplementedException();            
        }
    }
}
