using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public sealed class RtspMessageWriter : IMessageWriter<RtspInterleavedMessage>, IMessageWriter<RtspRequestMessage>
    {
        private readonly RtspStreamWriter _writer;
        private readonly RtspMessageValidator _validator;

        public RtspMessageWriter( IStream stream )
        {
            _writer = new RtspStreamWriter( stream );
            _validator = new RtspMessageValidator();
        }

        public void WriteMessage( RtspInterleavedMessage message )
        {
            _validator.ValidateMessage( message );

            _writer.WriteChar( '$' );
            _writer.WriteByte( (byte) (message.Length >> 8 & 0xFF) );
            _writer.WriteByte( (byte) (message.Length      & 0xFF) );
            _writer.Write( message.Buffer , 0 , message.Buffer.Length );
            _writer.Flush(); // this step is mandatory be design: pushing all buffered data outside the network adapter
        }

        public void WriteMessage( RtspRequestMessage message )
        {
            _validator.ValidateMessage( message );

            _writer.WriteLine( message.RequestLine.ToString() );

            foreach ( var header in message.Headers )
            {
                _writer.WriteLine( "{0}: {1}" , header.Key , header.Value );
            }

            _writer.WriteLine();

            if ( message.Body?.Length > 0 )
            {
                _writer.Write( message.Body , 0 , message.Body.Length );
            }

            _writer.Flush(); // this step is mandatory be design: pushing all buffered data outside the network adapter
        }
    }
}
