using System;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    public sealed class RtspClientRequestOptionsBuilder
    {
        private readonly Encoding _encoding;






        public RtspClientRequestOptionsBuilder()
            : this( Encoding.UTF8 )
        {
        }

        public RtspClientRequestOptionsBuilder( Encoding encoding )
        {
            _encoding = encoding ?? throw new ArgumentNullException( nameof( encoding ) );
        }








        public RtspClientRequestOptionsBuilder UseGlobalUri()
        {
            return SetUri( "*" );
        }

        public RtspClientRequestOptionsBuilder SetUri( string value )
        {
            throw new NotImplementedException();
        }

        public RtspClientRequestOptionsBuilder AddHeader( string name , string value )
        {
            throw new NotImplementedException();
        }

        public RtspClientRequestOptionsBuilder Headers( Action<RequestsRtspHeaderCollection> configurer )
        {
            throw new NotImplementedException();
        }

        public RtspClientRequestOptionsBuilder WriteBody( string value )
        {
            throw new NotImplementedException();
        }

        public RtspClientRequestOptionsBuilder WriteBody( string format , params object[] values )
        {
            throw new NotImplementedException();
        }

        public RtspClientRequestOptionsBuilder WriteBody( byte[] value )
        {
            throw new NotImplementedException();
        }

        public RtspClientRequestOptions Build()
        {
            throw new NotImplementedException();
        }
    }
}
