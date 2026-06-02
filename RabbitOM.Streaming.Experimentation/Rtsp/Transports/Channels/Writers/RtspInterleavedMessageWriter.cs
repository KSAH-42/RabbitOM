using System;
using System.Diagnostics;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public sealed class RtspInterleavedMessageWriter : IMessageWriter<RtspInterleavedMessage>
    {
        private readonly RtspStreamWriter _writer;

        public RtspInterleavedMessageWriter( IStream stream )
        {
            _writer = new RtspStreamWriter( stream );
        }

        public void WriteMessage( RtspInterleavedMessage message )
        {
            throw new NotImplementedException();
        }
    }
}
