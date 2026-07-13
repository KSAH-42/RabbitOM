using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed class RtspMessageCorrelation : IDisposable
    {
        private readonly TaskCompletionSource<RtspMessage> _tcs = new TaskCompletionSource<RtspMessage>( TaskCreationOptions.RunContinuationsAsynchronously );
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly uint _messageId;
        private int _disposed;




        public RtspMessageCorrelation( uint messageId )
        {
            _messageId = messageId;
        }





        public uint MessageId
        {
            get => _messageId;
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

        public void Cancel()
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






        public void SetMessage(RtspMessage message )
        {
            if ( message == null )
            {
                throw new ArgumentNullException( nameof( message ) );
            }

            if ( Volatile.Read( ref _disposed ) == 1 )
            {
                return;
            }

            _tcs.TrySetResult( message );
        }




        private void OnException( Exception exception )
        {
            System.Diagnostics.Debug.WriteLine( exception );
        }
    }
}
