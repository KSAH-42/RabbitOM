using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public class RtspClientRequestOptionsBuilder
    {
        public virtual RtspClientRequestOptionsBuilder UseGlobalUri()
        {
            return SetUri( "*" );
        }

        public virtual RtspClientRequestOptionsBuilder SetUri( string value )
        {
            throw new NotImplementedException();
        }

        public virtual RtspClientRequestOptionsBuilder AddHeader( string name , string value )
        {
            throw new NotImplementedException();
        }

        public virtual RtspClientRequestOptionsBuilder WriteBody()
        {
            throw new NotImplementedException();
        }

        public virtual RtspClientRequestOptionsBuilder WriteBody( string value )
        {
            throw new NotImplementedException();
        }

        public virtual RtspClientRequestOptionsBuilder WriteBody( byte[] value )
        {
            throw new NotImplementedException();
        }


        public virtual RtspClientRequestOptions Build()
        {
            throw new NotImplementedException();
        }
    }
}
