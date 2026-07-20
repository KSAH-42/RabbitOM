using System;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels.Writers
{
    public sealed class RtspMessageWriter : IMessageWriter<RtspInterleavedMessage> , IMessageWriter<RtspRequestMessage>
    {
        private readonly RtspStreamWriter _writer;
        private readonly RtspMessageWriterValidator _validator;

        public RtspMessageWriter( IStream stream )
        {
            _writer = new RtspStreamWriter( stream );
            _validator = new RtspMessageWriterValidator();
        }

        public void WriteMessage( RtspInterleavedMessage message )
        {
            _validator.Validate( message );

            _writer.WriteChar( '$' );
            _writer.WriteByte( message.Channel );
            _writer.WriteByte( (byte) (message.Length >> 8 & 0xFF) );
            _writer.WriteByte( (byte) (message.Length      & 0xFF) );
            _writer.Write( message.Buffer );
            _writer.Flush(); // mandatory by design: here we push all buffered data outside the nic adapter
        }

        public void WriteMessage( RtspRequestMessage message )
        {
            _validator.Validate( message );

            _writer.WriteLine( message.RequestLine.ToString() );

            foreach ( var header in message.Headers )
            {
                _writer.WriteLine( "{0}: {1}" , header.Key , header.Value );
            }

            _writer.WriteLine();
            _writer.Write( message.Body );
            _writer.Flush(); // mandatory by design: here we push all buffered data outside the nic adapter
        }
    }
}
