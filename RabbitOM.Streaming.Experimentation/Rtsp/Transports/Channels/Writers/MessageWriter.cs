using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public sealed class MessageWriter : IMessageWriter
    {
        private readonly IStream _stream;

        public MessageWriter( IStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }

        public void WriteMessage( RtspMessage message )
        {
            throw new NotImplementedException();
        }
    }
}
