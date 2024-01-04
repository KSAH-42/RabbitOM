using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public abstract class RTSPClient : IRTSPClient
    {
        public event EventHandler<RTSPCommunicationStartedEventArgs> CommunicationStarted;
        
        public event EventHandler<RTSPCommunicationStoppedEventArgs> CommunicationStopped;
       
        public event EventHandler<RTSPConnectedEventArgs> Connected;
        
        public event EventHandler<RTSPDisconnectedEventArgs> Disconnected;

        public event EventHandler<RTSPStreamingSetupEventArgs> StreamingSetup;

        public event EventHandler<RTSPStreamingStartedEventArgs> StreamingStarted;
        
        public event EventHandler<RTSPStreamingStoppedEventArgs> StreamingStopped;
        
        public event EventHandler<RTSPStreamingStatusChangedEventArgs> StreamingStatusChanged;
        
        public event EventHandler<RTSPMessageReceivedEventArgs> MessageReceived;
        
        public event EventHandler<RTSPPacketReceivedEventArgs> PacketReceived;
        
        public event EventHandler<RTSPErrorEventArgs> Error;





        ~RTSPClient()
        {
            Dispose( false );
        }




        public abstract object SyncRoot
        {
            get;
        }

        public abstract IRTSPClientConfiguration Configuration
        {
            get;
        }

        public abstract bool IsCommunicationStarted
        {
            get;
        }

        public abstract bool IsConnected
        {
            get;
        }

        public abstract bool IsStreamingStarted
        {
            get;
        }

        public abstract bool IsReceivingPacket
        {
            get;
        }

        public abstract bool IsDisposed
        {
            get;
        }





        public abstract bool StartCommunication();

        public abstract void StopCommunication();

        public abstract void StopCommunication( TimeSpan shutdownTimeout );

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }








        protected void RaiseEvent( EventArgs e )
        {
            if ( e is RTSPPacketReceivedEventArgs ) // For performance reason, let this condition statement at this position
            {
                OnPacketReceived( e as RTSPPacketReceivedEventArgs );
            }
            
            else if ( e is RTSPMessageReceivedEventArgs )
            {
                OnMessageReceived( e as RTSPMessageReceivedEventArgs );
            }

            else if ( e is RTSPCommunicationStartedEventArgs )
            {
                OnCommunicationStarted( e as RTSPCommunicationStartedEventArgs );
            }
            else if ( e is RTSPCommunicationStoppedEventArgs )
            {
                OnCommunicationStopped( e as RTSPCommunicationStoppedEventArgs );
            }
            else if ( e is RTSPConnectedEventArgs )
            {
                OnConnected( e as RTSPConnectedEventArgs );
            }
            else if ( e is RTSPDisconnectedEventArgs )
            {
                OnDisconnected( e as RTSPDisconnectedEventArgs );
            }
            else if ( e is RTSPStreamingSetupEventArgs )
            {
                OnStreamingSetup( e as RTSPStreamingSetupEventArgs );
            }
            else if ( e is RTSPStreamingStartedEventArgs )
            {
                OnStreamingStarted( e as RTSPStreamingStartedEventArgs );
            }
            else if ( e is RTSPStreamingStoppedEventArgs )
            {
                OnStreamingStopped( e as RTSPStreamingStoppedEventArgs );
            }
            else if ( e is RTSPStreamingStatusChangedEventArgs )
            {
                OnStreamingStatusChanged( e as RTSPStreamingStatusChangedEventArgs );
            }
            else if ( e is RTSPErrorEventArgs )
            {
                OnError( e as RTSPErrorEventArgs );
            }
        }








        protected virtual void OnCommunicationStarted( RTSPCommunicationStartedEventArgs e )
        {
            CommunicationStarted?.TryInvoke( this , e );
        }

        protected virtual void OnCommunicationStopped( RTSPCommunicationStoppedEventArgs e )
        {
            CommunicationStopped?.TryInvoke( this , e );
        }

        protected virtual void OnConnected( RTSPConnectedEventArgs e )
        {
            Connected?.TryInvoke( this , e );
        }

        protected virtual void OnDisconnected( RTSPDisconnectedEventArgs e )
        {
            Disconnected?.TryInvoke( this , e );
        }

        protected virtual void OnStreamingSetup( RTSPStreamingSetupEventArgs e )
        {
            StreamingSetup?.TryInvoke( this, e );
        }

        protected virtual void OnStreamingStarted( RTSPStreamingStartedEventArgs e )
        {
            StreamingStarted?.TryInvoke( this , e );
        }

        protected virtual void OnStreamingStopped( RTSPStreamingStoppedEventArgs e )
        {
            StreamingStopped?.TryInvoke( this , e );
        }

        protected virtual void OnStreamingStatusChanged( RTSPStreamingStatusChangedEventArgs e )
        {
            StreamingStatusChanged?.TryInvoke( this , e );
        }

        protected virtual void OnMessageReceived( RTSPMessageReceivedEventArgs e )
        {
            MessageReceived?.TryInvoke( this , e );
        }

        protected virtual void OnPacketReceived( RTSPPacketReceivedEventArgs e )
        {
            PacketReceived?.TryInvoke( this , e );
        }

        protected virtual void OnError( RTSPErrorEventArgs e )
        {
            Error?.TryInvoke( this , e );
        }
    }
}
