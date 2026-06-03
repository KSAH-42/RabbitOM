using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public sealed class RtspInterleavedMessageWriter : IMessageWriter<RtspInterleavedMessage>
    {
        private readonly RtspStreamWriter _writer;
        private readonly RtspInterleavedMessageValidator _validator;

        public RtspInterleavedMessageWriter( IStream stream )
        {
            _writer = new RtspStreamWriter( stream );
            _validator = new RtspInterleavedMessageValidator();
        }

        public void WriteMessage( RtspInterleavedMessage message )
        {
            _validator.ValidateMessage( message );

            _writer.WriteChar( '$' );
            _writer.WriteByte( (byte) (message.Length >> 8 & 0xFF) );
            _writer.WriteByte( (byte) (message.Length      & 0xFF) );
            _writer.Write( message.Buffer , 0 , message.Buffer.Length );
            _writer.Flush();
        }
    }
}
