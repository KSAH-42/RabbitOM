using System;
using System.Threading;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    using RabbitOM.Threading;

    internal sealed class RtspProxyRequestHandler : IDisposable
    {
        private readonly object _lock;
        private readonly ManualResetEvent _completionHandle;
        private readonly RtspMessageRequest _request;
        private RtspMessageResponse _response;
        private bool _succeed;
        private bool _isCanceled;



        public RtspProxyRequestHandler( RtspMessageRequest request )
        {
            _request          = request ?? throw new ArgumentNullException( nameof( request ) );
            _lock             = new object();
            _completionHandle = new ManualResetEvent( false );
        }



        public long RequestId
        {
            get => _request.Headers.FindByName<RtspHeaderCSeq>( RtspHeaderNames.CSeq )?.Value ?? 0;
        }

        public RtspMessageResponse Response
        {
            get => _response;
        }

        public bool IsCompleted
        {
            get => _completionHandle.TryWait( TimeSpan.Zero );
        }

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



        public void Cancel()
        {
            if ( ! _completionHandle.TryWait( TimeSpan.Zero ) )
            {
                OnCancel();

                _completionHandle.TrySet();
            }
        }

        public bool WaitCompletion( TimeSpan timeout )
        {
            return _completionHandle.TryWait( timeout );
        }

        public void HandleResponse( RtspMessageResponse response )
        {
            if ( _response != null || _completionHandle.TryWait( TimeSpan.Zero ) )
            {
                return;
            }

            _response = response;

            try
			{
                if ( _response == null || ! _response.TryValidate() )
                {
                    return;
                }

                var responseCSeq = _response.Headers.FindByName<RtspHeaderCSeq>( RtspHeaderNames.CSeq );

                if ( responseCSeq == null || !responseCSeq.TryValidate() )
                {
                    return;
                }

                var requestCSeq = _request.Headers.FindByName<RtspHeaderCSeq>( RtspHeaderNames.CSeq );

                if ( requestCSeq == null || ! requestCSeq.TryValidate() )
                {
                    return;
                }

                if ( requestCSeq.Value != responseCSeq.Value )
                {
                    return;
                }

                OnSucceed();
            }
			finally
			{
                _completionHandle.TrySet();
			}
        }

        public void Dispose()
        {
            _completionHandle.Dispose();
        }



        private void OnCancel()
        {
            lock( _lock )
            {
                _isCanceled = true;
                _succeed    = false;
            }
        }

        private void OnSucceed()
        {
            lock ( _lock )
            {
                _succeed = true;
            }
        }
    }
}
