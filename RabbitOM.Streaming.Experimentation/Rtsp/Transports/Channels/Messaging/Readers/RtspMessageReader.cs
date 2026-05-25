using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    public sealed class RtspMessageReader : IMessageReader
    {
        private readonly IStream _stream;
        private readonly RtspRequestMessageReader _requestReader;
        private readonly RtspResponseMessageReader _responseReader;
        private readonly RtspInterleaveMessageReader _interleavedReader;        




        // so we don't use a pipereader class, instead a special stream
        // with some custom read optimizations and additional methods

        public RtspMessageReader( IStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
         
            _requestReader = new RtspRequestMessageReader( stream );
            _responseReader = new RtspResponseMessageReader( stream );
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

            var startLine = _stream.ReadLine();

            if ( startLine != null )
            {
                if ( _responseReader.CanReadMessage( startLine ) )
                {
                    return _responseReader.ReadMessage( startLine );
                }

                if ( _requestReader.CanReadMessage( startLine ) )
                {
                    return _requestReader.ReadMessage( startLine );
                }

                throw new InvalidDataException();
            }
            
            return null;
        }
    }
}
