using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitOM.Streaming.RtspV2.Transports.Channels
{
    public abstract class RtspClientChannel : IChannel
    {
        public event EventHandler Opened;

        public event EventHandler Closed;

        public event EventHandler Aborted;

        public event EventHandler Faulted;

        public event EventHandler<RtspMessageEventArgs> MessageReceived;




        ~RtspClientChannel()
        {
            Dispose( false );
        }




        public abstract bool IsOpened { get; }

        public abstract bool IsFaulted { get; }

        public bool IsDisposed { get; private set; }







        public abstract void Open();

        public abstract void Close();

        public abstract void Abort();

        public abstract Task OpenAsync();

        public abstract Task CloseAsync();

        public abstract Task AbortAsync();

        public abstract Task SendMessage( RtspInterleavedMessage interleavedData );

        public abstract RtspResponseMessage SendMessage( RtspRequestMessage request );

        public abstract Task SendMessageAsync( RtspInterleavedMessage interleavedData , CancellationToken cancellationToken );

        public abstract Task<RtspResponseMessage> SendMessageAsync( RtspRequestMessage request , CancellationToken cancellationToken );

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
                Close();
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

        protected virtual void OnFaulted( EventArgs e )
        {
            Faulted?.TryInvoke( this , e );
        }

        protected virtual void OnMessageReceived( RtspMessageEventArgs e )
        {
            MessageReceived?.TryInvoke( this , e );
        }
    }
}
