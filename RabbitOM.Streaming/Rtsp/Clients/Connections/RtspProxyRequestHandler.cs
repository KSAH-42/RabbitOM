using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent a request handler
    /// </summary>
    internal sealed class RtspProxyRequestHandler : IDisposable
    {
        private readonly object                _lock               = null;

        private bool                           _succeed            = false;

        private bool                           _isCanceled         = false;

        private readonly RtspEventWaitHandle   _completionHandle   = null;

        private readonly RtspMessageRequest    _request            = null;

        private RtspMessageResponse            _response           = null;






        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="request">the request</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspProxyRequestHandler( RtspMessageRequest request )
        {
            _request = request ?? throw new ArgumentNullException( nameof( request ) );
            _lock = new object();
            _completionHandle = new RtspEventWaitHandle();
        }





        /// <summary>
        /// Gets the sequence identifier
        /// </summary>
        public long RequestId
        {
            get => _request.Headers.FindByName<RtspHeaderCSeq>( RtspHeaderNames.CSeq )?.Value ?? 0;
        }

        /// <summary>
        /// Gets the response
        /// </summary>
        public RtspMessageResponse Response
        {
            get => _response;
        }

        /// <summary>
        /// Check if completion status
        /// </summary>
        public bool IsCompleted
        {
            get => _completionHandle.Wait( TimeSpan.Zero );
        }

        /// <summary>
        /// Gets the status
        /// </summary>
        public bool Succeed
        {
            get
            {
                lock (_lock)
                {
                    return _succeed;
                }
            }
        }

        /// <summary>
        /// Check if the handler has been canceled
        /// </summary>
        public bool IsCanceled
        {
            get
            {
                lock (_lock)
                {
                    return _isCanceled;
                }
            }
        }









        /// <summary>
        /// Cancel the handle operation
        /// </summary>
        public void Cancel()
        {
            if ( !_completionHandle.Wait( TimeSpan.Zero ) )
            {
                OnCancel();

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
        public void HandleResponse( RtspMessageResponse response )
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
        private void InternalHandleResponse( RtspMessageResponse response )
        {
            _response = response;

            if ( _response == null || !_response.TryValidate() )
            {
                return;
            }

            var responseCSeq = _response.Headers.FindByName<RtspHeaderCSeq>( RtspHeaderNames.CSeq );

            if ( responseCSeq == null || !responseCSeq.TryValidate() )
            {
                return;
            }

            var requestCSeq = _request.Headers.FindByName<RtspHeaderCSeq>( RtspHeaderNames.CSeq );

            if ( requestCSeq == null || !requestCSeq.TryValidate() )
            {
                return;
            }

            if ( requestCSeq.Value != responseCSeq.Value )
            {
                return;
            }

            OnSucceed();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _completionHandle.Dispose();
        }









        /// <summary>
        /// Occurs when cancelation is needed
        /// </summary>
        private void OnCancel()
        {
            lock( _lock )
            {
                _isCanceled = true;
                _succeed    = false;
            }
        }

        /// <summary>
        /// Occurs on success
        /// </summary>
        private void OnSucceed()
        {
            lock ( _lock )
            {
                _succeed = true;
            }
        }
    }
}
