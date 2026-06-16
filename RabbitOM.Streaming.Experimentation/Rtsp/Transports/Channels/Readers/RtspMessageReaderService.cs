using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspMessageReaderService
    {
        private readonly IMessageReader _reader;

        public RtspMessageReaderService( IMessageReader reader )
        {
            _reader = reader ?? throw new ArgumentNullException( nameof( reader ) );
        }

        public RtspMessage ReadMessage()
        {
            var prefix = _reader.Peek();

            if ( ! prefix.HasValue )
            {
                return null;
            }

            if ( prefix.Value == '$' )
            {
                return _reader.ReadInterleavedMessage();
            }

            return _reader.ReadControlMessage();
        }
    }
}
