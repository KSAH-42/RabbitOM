using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public sealed class RtspRequestMessageWriter : IMessageWriter<RtspRequestMessage>
    {
        private readonly RtspStreamWriter _writer;

        public RtspRequestMessageWriter( IStream stream )
        {
            _writer = new RtspStreamWriter( stream );
        }

        public void WriteMessage( RtspRequestMessage message )
        {
        }
    }
}
