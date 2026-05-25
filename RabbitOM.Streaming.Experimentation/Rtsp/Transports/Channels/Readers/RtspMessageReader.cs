using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspMessageReader : IMessageReader
    {
        private readonly IStream _stream;
        private readonly RtspRequestResponseMessageReader _requestResponseReader;
        private readonly RtspInterleaveMessageReader _interleavedReader;        




        // so we don't use a pipereader class, instead a special stream
        // with some custom read optimizations and additional methods

        public RtspMessageReader( IStream stream )
        {
            if ( _stream == null )
            {
                throw new ArgumentNullException( nameof( stream ) );
            }

            _stream = stream;
            _requestResponseReader = new RtspRequestResponseMessageReader( stream );
            _interleavedReader = new RtspInterleaveMessageReader( stream );
        }




        public RtspMessage ReadMessage()
        {
            var prefix = _stream.PeekByte();

            if ( prefix <= 0 )
            {
                return null;
            }

            if ( prefix == '$' )
            {
                return _interleavedReader.ReadMessage();
            }

            return _requestResponseReader.ReadMessage( _stream.ReadLine() );
        }
    }
}
