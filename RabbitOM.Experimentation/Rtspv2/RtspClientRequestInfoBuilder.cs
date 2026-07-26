using System;
using System.Text;

namespace RabbitOM.Streaming.RtspV2
{
    using RabbitOM.Streaming.RtspV2.Headers;

    // TODO: this is not probably a good name for this class, because it's introduce confusion of other rtsp method which different from the <seealso cref="RtspClient.OptionsAsync(RtspClientRequestOptions)"/>, find a different where the option word will not be Params, Args, Metadata ... but it should be related to optional things, so how we can name the arg used by rtsp method ? RtspRequestXXX and then we getRtspRequestXXXBuilder
    public sealed class RtspClientRequestInfoBuilder
    {
        private readonly Encoding _encoding;






        public RtspClientRequestInfoBuilder()
            : this( Encoding.UTF8 )
        {
        }

        public RtspClientRequestInfoBuilder( Encoding encoding )
        {
            _encoding = encoding ?? throw new ArgumentNullException( nameof( encoding ) );
        }








        public RtspClientRequestInfoBuilder UseGlobalUri()
        {
            return SetUri( "*" );
        }

        public RtspClientRequestInfoBuilder SetUri( string value )
        {
            throw new NotImplementedException();
        }

        public RtspClientRequestInfoBuilder Headers( Action<RequestsRtspHeaderCollection> configurer )
        {
            throw new NotImplementedException();
        }

        public RtspClientRequestInfoBuilder WriteBody( string value )
        {
            throw new NotImplementedException();
        }

        public RtspClientRequestInfoBuilder WriteBody( string format , params object[] values )
        {
            throw new NotImplementedException();
        }

        public RtspClientRequestInfoBuilder WriteBody( byte[] value )
        {
            throw new NotImplementedException();
        }

        public RtspClientRequestInfo Build()
        {
            throw new NotImplementedException();
        }
    }
}
