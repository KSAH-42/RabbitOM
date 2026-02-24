using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Receivers
{
    public abstract class RtspMediaReceiver : IMediaReceiver , IDisposable
    {
        public event EventHandler<RtspCommunicationStartedEventArgs> CommunicationStarted;
        
        public event EventHandler<RtspCommunicationStoppedEventArgs> CommunicationStopped;
        
        public event EventHandler<RtspConnectedEventArgs> Connected;
        
        public event EventHandler<RtspDisconnectedEventArgs> Disconnected;
        
        public event EventHandler<RtspStreamingStartedEventArgs> StreamingStarted;
        
        public event EventHandler<RtspStreamingStoppedEventArgs> StreamingStopped;
        
        public event EventHandler<RtspStreamingStatusChangedEventArgs> StreamingStatusChanged;
        
        public event EventHandler<RtspDataReceivedEventArgs> DataReceived;
        
        public event EventHandler<RtspErrorEventArgs> Error;
        







        ~RtspMediaReceiver()
        {
            Dispose( false );
        }
        







        public abstract bool IsCommunicationStarted { get; }
        
        public abstract bool IsCommunicationStopping { get;}
       
        public abstract bool IsConnected { get; }
        
        public abstract bool IsStreamingStarted { get; }
        
        public abstract bool IsReceivingData { get; }








        public abstract bool StartCommunication();

        public abstract void StopCommunication();

        public abstract void StopCommunication(TimeSpan timeout);

        public abstract void BeginStopCommunication();

        public abstract bool EndStopCommunication(TimeSpan timeout);


        public void Dispose()
        {
            Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }
        







        protected virtual void OnCommunicationStarted(RtspCommunicationStartedEventArgs e)
        {
            CommunicationStarted?.TryInvoke( this , e );
        }

        protected virtual void OnCommunicationStopped(RtspCommunicationStoppedEventArgs e)
        {
            CommunicationStopped?.TryInvoke( this , e );
        }

        protected virtual void OnConnected(RtspConnectedEventArgs e)
        {
            Connected?.TryInvoke( this , e );
        }

        protected virtual void OnDisconnected(RtspDisconnectedEventArgs e)
        {
            Disconnected?.TryInvoke( this , e );
        }

        protected virtual void OnStreamingStarted(RtspStreamingStartedEventArgs e)
        {
            StreamingStarted?.TryInvoke( this , e );
        }

        protected virtual void OnStreamingStopped(RtspStreamingStoppedEventArgs e)
        {
            StreamingStopped?.TryInvoke( this , e );
        }

        protected virtual void OnStreamingStatusChanged(RtspStreamingStatusChangedEventArgs e)
        {
            StreamingStatusChanged?.TryInvoke( this , e );
        }

        protected virtual void OnDataReceived(RtspDataReceivedEventArgs e)
        {
            DataReceived?.TryInvoke( this , e );
        }

        protected virtual void OnError(RtspErrorEventArgs e)
        {
            Error?.TryInvoke( this , e );
        }
    }
}
