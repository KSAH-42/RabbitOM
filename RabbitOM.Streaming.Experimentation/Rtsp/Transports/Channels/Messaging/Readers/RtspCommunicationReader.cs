using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    internal sealed class RtspCommunicationReader : IMessageReader
    {
        private readonly RtspStream _stream;
        
        public RtspCommunicationReader( RtspStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public RtspMessage ReadMessage()
        {
            throw new NotImplementedException();            
        }
    }
}
