using System;

namespace RabbitOM.Net.Rtsp.Remoting
{
    /// <summary>
    /// Represent the invoker result
    /// </summary>
    public sealed class RTSPInvokerResult
    {
        private readonly bool                      _succeed   = false;

        private readonly RTSPInvokerResultResponse _response  = null;

        private readonly RTSPInvokerResultRequest  _request   = null;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="succeed">the success</param>
        /// <param name="request">the request</param>
        /// <param name="response">the response</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPInvokerResult( bool succeed , RTSPInvokerResultRequest request , RTSPInvokerResultResponse response )
        {
            _succeed = succeed;
            _request = request ?? throw new ArgumentNullException( nameof( request ) );
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
        public RTSPInvokerResultRequest Request
        {
            get => _request;
        }

        /// <summary>
        /// Gets the response
        /// </summary>
        public RTSPInvokerResultResponse Response
        {
            get => _response;
        }
    }
}
