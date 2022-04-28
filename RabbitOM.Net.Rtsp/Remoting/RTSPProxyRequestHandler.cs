using System;

namespace RabbitOM.Net.Rtsp.Remoting
{
    /// <summary>
    /// Represent a request handler
    /// </summary>
    public sealed class RTSPProxyRequestHandler
    {
        private readonly RTSPDataMember<bool>  _succeed            = null;

        private readonly RTSPDataMember<bool>  _isCanceled         = null;

        private readonly RTSPEventWaitHandle   _completionHandle   = null;

        private readonly RTSPMessageRequest    _request            = null;

        private RTSPMessageResponse            _response           = null;






        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="request">the request</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPProxyRequestHandler( RTSPMessageRequest request )
        {
            _request = request ?? throw new ArgumentNullException( nameof( request ) );
            _succeed = new RTSPDataMember<bool>( false );
            _isCanceled = new RTSPDataMember<bool>( false );
            _completionHandle = new RTSPEventWaitHandle();
        }





        /// <summary>
        /// Gets the sequence identifier
        /// </summary>
        public long RequestId
        {
            get => _request.Headers.FindByName<RTSPHeaderCSeq>( RTSPHeaderNames.CSeq )?.Value ?? 0;
        }

        /// <summary>
        /// Gets the response
        /// </summary>
        public RTSPMessageResponse Response
        {
            get => _response;
        }

        /// <summary>
        /// Gets the status
        /// </summary>
        public bool Succeed
        {
            get => _succeed.Value;
        }

        /// <summary>
        /// Check if the handler has been canceled
        /// </summary>
        public bool IsCanceled
        {
            get => _isCanceled.Value;
        }

        /// <summary>
        /// Check if completion status
        /// </summary>
        public bool IsCompleted
        {
            get => _completionHandle.Wait( TimeSpan.Zero );
        }




        /// <summary>
        /// Cancel the handle operation
        /// </summary>
        public void Cancel()
        {
            if ( !_completionHandle.Wait( TimeSpan.Zero ) )
            {
                _isCanceled.Value = true;
                _succeed.Value = false;

                _completionHandle.Set();
            }
        }

        /// <summary>
        /// Wait the completion
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool WaitCompletion( TimeSpan timeout )
        {
            return _completionHandle.Wait( timeout );
        }

        /// <summary>
        /// Handle the response
        /// </summary>
        /// <param name="response">the response</param>
        public void HandleResponse( RTSPMessageResponse response )
        {
            if ( _response != null || _completionHandle.Wait( TimeSpan.Zero ) )
            {
                return;
            }
                                  
            InternalHandleResponse( response );
            _completionHandle.Set();
        }

        /// <summary>
        /// Update internal member according to the response value
        /// </summary>
        /// <param name="response">the response</param>
        private void InternalHandleResponse( RTSPMessageResponse response )
        {
            _response = response;

            if ( _response == null || !_response.Validate() )
            {
                return;
            }

            var responseCSeq = _response.Headers.FindByName<RTSPHeaderCSeq>( RTSPHeaderNames.CSeq );

            if ( responseCSeq == null || !responseCSeq.Validate() )
            {
                return;
            }

            var requestCSeq = _request.Headers.FindByName<RTSPHeaderCSeq>( RTSPHeaderNames.CSeq );

            if ( requestCSeq == null || !requestCSeq.Validate() )
            {
                return;
            }

            if ( requestCSeq.Value != responseCSeq.Value )
            {
                return;
            }

            _succeed.Value = true;
        }
    }
}
