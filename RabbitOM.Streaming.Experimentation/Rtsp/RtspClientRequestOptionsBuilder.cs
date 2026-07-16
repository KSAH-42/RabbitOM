using System;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers;

    /// TODO: this is not probably a good name for this class, because it's introduce confusion of other rtsp method which different from the <seealso cref="RtspClient.OptionsAsync(RtspClientRequestOptions)"/>, find a different where the option word will not be Params, Args, Metadata ... but it should be related to optional things, so how we can name the arg used by rtsp method ? RtspRequestXXX and then we getRtspRequestXXXBuilder
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
