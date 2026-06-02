using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspMessageReader : IMessageReader
    {
        private readonly RtspStreamReader _reader;
        private readonly IMessageReader _requestResponseReader;
        private readonly IMessageReader _interleavedReader;


        public RtspMessageReader( IStream stream , IMessageReader requestResponseReader , IMessageReader interleavedReader )
        {
            _reader = new RtspStreamReader( stream );
            _requestResponseReader = requestResponseReader ?? throw new ArgumentNullException( nameof( requestResponseReader ) );
            _interleavedReader = interleavedReader ?? throw new ArgumentNullException( nameof( interleavedReader ) );
        }


        public RtspMessage ReadMessage()
        {
            var prefix = _reader.ReadByte();

            if ( prefix <= 0 )
            {
                return null;
            }

            if ( prefix == '$' )
            {
                return _interleavedReader.ReadMessage();
            }

            return _requestResponseReader.ReadMessage();
        }
    }
}
