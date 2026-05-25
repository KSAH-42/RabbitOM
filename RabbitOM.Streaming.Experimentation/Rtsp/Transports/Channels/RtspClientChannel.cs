using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Messaging;

    public abstract class RtspClientChannel : IClientChannel
    {        
        public event EventHandler Opened;

        public event EventHandler Closed;

        public event EventHandler Aborted;

        public event EventHandler<RtspRequestMessageEventArgs> RequestSended;

        public event EventHandler<RtspResponseMessageEventArgs> ResponseReceived;






        ~RtspClientChannel()
        {
            Dispose( false );
        }





        public abstract EndPoint EndPoint { get; }

        public abstract bool IsOpened { get; }








        public abstract void Open();
        
        public abstract void Close();

        public abstract void Abort();

        public abstract void SendMessage( RtspInterleaveMessage interleavedData );
        
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

        protected virtual void OnRequestSended( RtspRequestMessageEventArgs e )
        {
            RequestSended?.TryInvoke( this , e );
        }

        protected virtual void OnResponseReceived( RtspResponseMessageEventArgs e )
        {
            ResponseReceived?.TryInvoke( this , e );
        }
    }
}
