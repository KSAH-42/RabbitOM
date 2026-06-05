using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspMessageReader : IMessageReader
    {
        private readonly RtspMessageReaderInternal _reader;


        public RtspMessageReader( IStream stream )
        {
            _reader = new RtspMessageReaderInternal( stream );
        }


        public RtspMessage ReadMessage()
        {
            var prefix = _reader.PeekValue();

            if ( prefix < 0 )
            {
                return null;
            }

            if ( prefix == '$' )
            {
                return _reader.ReadMessageAsInterleaved();
            }

            return _reader.ReadMessage();
        }
    }
}
