using System;

namespace RabbitOM.Net.RtspV2
{
    using RabbitOM.Net.RtspV2.Headers;

    // TODO: this is not probably a good name for this class, because it's introduce confusion of other rtsp method which different from the <seealso cref="RtspClient.OptionsAsync(RtspClientRequestOptions)"/>, find a different where the option word will not be Params, Args, Metadata ... but it should be related to optional things, so how we can name the arg used by rtsp method ? RtspRequestXXX and then we getRtspRequestXXXBuilder
    // TODO: check if we need to used System.Text.Encoding class internally probably when writing string on the body
    public sealed class RtspClientRequestInfoBuilder
    {
        public RtspClientRequestInfoBuilder UseGlobalUri()
        {
            return SetUri( "*" );
        }

        public RtspClientRequestInfoBuilder SetUri( string value )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public RtspClientRequestInfoBuilder Headers( Action<RequestsRtspHeaderCollection> configurer )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public RtspClientRequestInfoBuilder WriteBody( string value )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public RtspClientRequestInfoBuilder WriteBody( string format , params object[] values )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public RtspClientRequestInfoBuilder WriteBody( byte[] value )
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public RtspClientRequestInfo Build()
        {
            throw new NotImplementedException( "To be implemented" );
        }
    }
}
