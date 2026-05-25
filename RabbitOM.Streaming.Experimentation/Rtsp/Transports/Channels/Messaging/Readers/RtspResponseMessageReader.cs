using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    public sealed class RtspResponseMessageReader
    {
        private readonly RtspStream _stream;
        
        public RtspResponseMessageReader( RtspStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public bool CanReadMessage( string startLine )
        {
            throw new NotImplementedException();            
        }

        public RtspMessage ReadMessage( string startLine )
        {
            throw new NotImplementedException();            
        }
    }
}
