using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels
{
    public sealed class RtspMessageCorrelation : IDisposable
    {
        private readonly TaskCompletionSource<RtspMessage> _tcs = new TaskCompletionSource<RtspMessage>( TaskCreationOptions.RunContinuationsAsynchronously );

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private readonly long _cseq;

        private int _disposed;






        public RtspMessageCorrelation( long cseq )
        {
            _cseq = cseq;
        }






        public long CSeq
        {
            get => _cseq;
        }






        public bool TryReceive( TimeSpan timeout , out RtspMessage result )
        {
            result = null;

            if ( Volatile.Read( ref _disposed ) == 1 )
            {
                return false;
            }

            try
            {
                var task = _tcs.Task;

                if ( ! task.Wait( (int) timeout.TotalMilliseconds , _cts.Token) )
                {
                    return false;
                }

                if ( ! task.IsCompleted || task.IsFaulted )
                {
                    return false;
                }

                result = task.Result;

                return result != null;
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        public void CompleteOperation( RtspMessage message )
        {
            OnValidate( message );

            if ( Volatile.Read( ref _disposed ) == 1 )
            {
                return;
            }

            _tcs.TrySetResult( message );
        }

        public void CancelOperation()
        {
            try
            {
                _cts.Cancel();
            }
            catch( Exception ex )
            {
                OnException( ex  );
            }
        }

        public void Dispose()
        {
            if ( Interlocked.Exchange( ref _disposed , 1 ) == 0 )
            {
                _cts.Cancel();
                _cts.Dispose();
                _tcs.TrySetCanceled();
            }
        }








        private void OnValidate( RtspMessage message )
        {
            if ( message == null )
            {
                throw new ArgumentNullException( nameof( message ) );
            }

            if ( message is RtspInterleavedMessage )
            {
                throw new ArgumentException( "it should never happens, someone are doing something with the source code" , nameof( message ) );
            }

            if ( message is RtspRequestMessage request && request.Headers.CSeq != CSeq )
            {
                throw new ArgumentException( "wrong request.cseq: it should never happens, there is an issue somewhere" , nameof( message ) );
            }

            if ( message is RtspResponseMessage response && response.Headers.CSeq != CSeq )
            {
                throw new ArgumentException( "wrong response.cseq: it should never happens, there is an issue somewhere" , nameof( message ) );
            }
        }

        private void OnException( Exception exception )
        {
            System.Diagnostics.Debug.WriteLine( exception );
        }
    }
}
