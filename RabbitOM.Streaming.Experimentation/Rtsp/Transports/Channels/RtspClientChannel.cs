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








        public abstract Task OpenAsync();

        public abstract Task CloseAsync();

        public abstract Task AbortAsync();

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
                CloseAsync().GetAwaiter().GetResult(); // take care dispose should not throw exception
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
