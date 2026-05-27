using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspMessageReader : IMessageReader
    {
        private readonly IStream _stream;
        private readonly RtspRequestResponseMessageReader _requestResponseReader;
        private readonly RtspInterleaveMessageReader _interleavedReader;    


        public RtspMessageReader( IStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
            _requestResponseReader = new RtspRequestResponseMessageReader( stream );
            _interleavedReader = new RtspInterleaveMessageReader( stream );
        }


        public RtspMessage ReadMessage()
        {
            var prefix = _stream.PeekByte();

            if ( prefix > 0 )
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
