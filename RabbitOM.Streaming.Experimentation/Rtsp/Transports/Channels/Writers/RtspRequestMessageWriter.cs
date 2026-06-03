using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Writers
{
    public sealed class RtspRequestMessageWriter : IMessageWriter<RtspRequestMessage>
    {
        private readonly RtspStreamWriter _writer;
        private readonly RtspRequestMessageValidator _validator;

        public RtspRequestMessageWriter( IStream stream )
        {
            _writer = new RtspStreamWriter( stream );
            _validator = new RtspRequestMessageValidator();
        }

        public void WriteMessage( RtspRequestMessage request )
        {
            _validator.ValidateRequest( request );

            _writer.WriteLine( request.RequestLine.ToString() );

            foreach ( var header in request.Headers )
            {
                _writer.WriteLine( "{0}: {1}" , header.Key , header.Value );
            }

            _writer.WriteLine();

            if ( request.Body?.Length > 0 )
            {
                _writer.Write( request.Body , 0 , request.Body.Length );
            }

            _writer.Flush();
        }
    }
}
