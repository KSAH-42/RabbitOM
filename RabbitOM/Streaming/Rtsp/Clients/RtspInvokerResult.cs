using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspInvokerResult
    {
        private readonly RtspInvokerResultResponse _response;
        private readonly RtspInvokerResultRequest _request;
        private readonly bool _succeed;


        public RtspInvokerResult( bool succeed , RtspInvokerResultRequest request , RtspInvokerResultResponse response )
        {
            _succeed  = succeed;
            _request  = request  ?? throw new ArgumentNullException( nameof( request  ) );
            _response = response ?? throw new ArgumentNullException( nameof( response ) );
        }


        public bool Succeed
        {
            get => _succeed;
        }

        public RtspInvokerResultRequest Request
        {
            get => _request;
        }

        public RtspInvokerResultResponse Response
        {
            get => _response;
        }
    }
}
