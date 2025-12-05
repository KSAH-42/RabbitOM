using System;

namespace RabbitOM.Streaming.Net.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent the invoker result
    /// </summary>
    public sealed class RtspInvokerResult
    {
        private readonly RtspInvokerResultResponse _response;

        private readonly RtspInvokerResultRequest _request;

        private readonly bool _succeed;





        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="succeed">the success</param>
        /// <param name="request">the request</param>
        /// <param name="response">the response</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspInvokerResult( bool succeed , RtspInvokerResultRequest request , RtspInvokerResultResponse response )
        {
            _succeed  = succeed;
            _request  = request  ?? throw new ArgumentNullException( nameof( request  ) );
            _response = response ?? throw new ArgumentNullException( nameof( response ) );
        }





        /// <summary>
        /// Gets the success status
        /// </summary>
        public bool Succeed
        {
            get => _succeed;
        }

        /// <summary>
        /// Gets the request
        /// </summary>
        public RtspInvokerResultRequest Request
        {
            get => _request;
        }

        /// <summary>
        /// Gets the response
        /// </summary>
        public RtspInvokerResultResponse Response
        {
            get => _response;
        }
    }
}
