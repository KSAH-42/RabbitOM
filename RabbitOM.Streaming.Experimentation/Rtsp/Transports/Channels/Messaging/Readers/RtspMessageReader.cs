using System;
using System.IO;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging.Readers
{
    public sealed class RtspMessageReader : IMessageReader
    {
        private readonly Stream _stream;
        private readonly IMessageReader _communicationReader;
        private readonly IMessageReader _interleavedReader;        


        public RtspMessageReader( Stream stream , IMessageReader communicationReader , IMessageReader interleaveReader )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
            _communicationReader = communicationReader ?? throw new ArgumentNullException( nameof( communicationReader ) );
            _interleavedReader = interleaveReader ?? throw new ArgumentNullException( nameof( interleaveReader ) );
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
                return _interleavedReader.ReadMessage();
            }
            
            // stream must support seeking or peek method
            // do we need to use a stream or use interface instead ?
            // the communication reader should be able to go back and
            // not nesserary the interleaved reader
            // used IXXXXXXXXXXXXX with a peek Message instead of using a stream ? 
            // adding a IMessageStream with buffering caps ???
            // for solving Peek() method or seeking, that can cause issue for
            // others reader that need to seek backwards
            // avoid solid interface segration
            // avoid passing args
            // avoid using PipeReader class => adding nuget package ....
            // a custom class inject can fix the problem
            // but find a better approach
            
            return _communicationReader.ReadMessage();
        }
    }
}
