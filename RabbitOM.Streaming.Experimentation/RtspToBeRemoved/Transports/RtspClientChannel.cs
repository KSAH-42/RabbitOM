using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Transports
{
    public abstract class RtspClientChannel : IClientChannel
    {        
        public event EventHandler Opened;

        public event EventHandler Closed;

        public event EventHandler Aborted;






        ~RtspClientChannel()
        {
            Dispose( false );
        }





        public abstract string Address { get; set; }
        
        public abstract TimeSpan ReceiveTimeout { get; set; }
        
        public abstract TimeSpan SendTimeout { get; set; }

        public abstract bool IsOpened { get; }






        public abstract void Open();
        
        public abstract void Close();

        public abstract void Abort();

        public abstract RtspResponseMessage SendMessage( RtspRequestMessage request );

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( disposing )
            {
                Close();
            }
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
    }
}
