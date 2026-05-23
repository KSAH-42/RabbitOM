using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    public sealed class RtspMessageReader : IMessageReader
    {
        private readonly Stream _stream;
        private readonly RtspInterleaveMessageReader _interleaveMessageReader;
        private readonly RtspRequestMessageReader _requestMessageReader;
        private readonly RtspResponseMessageReader _responseMessageReader;
        

        public RtspMessageReader( Stream stream )
        {
            if ( stream == null )
            {
                throw new ArgumentNullException( nameof( stream ) );
            }

            _stream = stream;
            _interleaveMessageReader = new RtspInterleaveMessageReader( _stream );
            _requestMessageReader = new RtspRequestMessageReader( _stream );
            _responseMessageReader = new RtspResponseMessageReader( _stream );
        }


        public RtspMessage ReadMessage()
        {
            var prefix = _stream.ReadByte();

            if ( prefix <= 0 )
            {
                return null;
            }

            if ( prefix == '$' )
            {
                return _interleaveMessageReader.ReadMessage();
            }

            var startLine = $"{(char)prefix}{_stream.ReadLine()}";
            
            if ( _requestMessageReader.CanReadMessage( startLine ) )
            {
                return _requestMessageReader.ReadMessage( startLine );
            }

            if ( _responseMessageReader.CanReadMessage( startLine ) )
            {
                return _responseMessageReader.ReadMessage( startLine );
            }
 
            throw new InvalidDataException();
        }
    }
}
