using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public abstract class RtspClientChannel : IClientChannel
    {
        public event EventHandler Opened;

        public event EventHandler Closed;

        public event EventHandler Aborted;

        public event EventHandler<RtspMessageEventArgs> MessageReceived;



        ~RtspClientChannel()
        {
            Dispose( false );
        }




        public abstract bool IsOpened { get; }

        public bool IsDisposed { get; private set; }








        public abstract Task OpenAsync( CancellationTokenSource cancellationToken = default );

        public abstract Task CloseAsync( CancellationTokenSource cancellationToken = default );

        public abstract Task AbortAsync( CancellationTokenSource cancellationToken = default );

        public abstract Task SendMessageAsync( RtspInterleavedMessage interleavedData , CancellationTokenSource cancellationToken = default );

        public abstract Task<RtspResponseMessage> SendMessageAsync( RtspRequestMessage request , CancellationTokenSource cancellationToken = default );

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( IsDisposed )
            {
                return;
            }

            if ( disposing )
            {
                CloseAsync().GetAwaiter().GetResult();
            }

            IsDisposed = true;
        }




        protected virtual void OnOpened( EventArgs e )
        {
            Opened?.TryInvoke( this , e );
        }

        protected virtual void OnClosed( EventArgs e )
        {
            Closed?.TryInvoke( this , e );
        }

        protected virtual void OnAborted( EventArgs e )
        {
            Aborted?.TryInvoke( this , e );
        }

        protected virtual void OnMessageReceived( RtspMessageEventArgs e )
        {
            MessageReceived?.TryInvoke( this , e );
        }
    }
}
