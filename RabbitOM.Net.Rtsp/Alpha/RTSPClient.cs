using System;
using System.Threading.Tasks;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public class RTSPClient : IRTSPClient
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





        private readonly RTSPEventDispatcher _dispatcher;
       
        private readonly RTSPMediaChannel _channel;
        
        private readonly RTSPThread _thread;





        public RTSPClient()
        {
            _dispatcher = new RTSPEventDispatcher( RaiseEvent );
            _channel = new RTSPMediaChannel( _dispatcher );
            _thread = new RTSPThread( "RTSP - Client thread" );
        }





        public object SyncRoot
        {
            get => _channel.SyncRoot;
        }

        public IRTSPClientConfiguration Configuration
        {
            get => _channel.Configuration;
        }

        public bool IsDisposed
        {
            get => _channel.IsDisposed;
        }

        public bool IsConnected
        {
            get => _channel.IsConnected;
        }

        public bool IsReceivingPacket
        {
            get => _channel.IsReceivingPacket;
        }

        public bool IsStreamingStarted
        {
            get => _channel.IsPlaying;
        }

        public bool IsCommunicationStarted
        {
            get => _thread.IsStarted;
        }





        public bool StartCommunication()
        {
            _dispatcher.Start();
            
            return _thread.Start( () =>
            {
                OnCommunicationStarted( new RTSPCommunicationStartedEventArgs() );

                using ( var runner = new RTSPMediaChannelRunner( _channel ) )
                {
                    while ( _thread.CanContinue( runner.IdleTimeout ) )
                    {
                        runner.Run();
                    }
                }

                OnCommunicationStopped( new RTSPCommunicationStoppedEventArgs() );
            });
        }

        public void StopCommunication()
        {
            _thread.Stop();

            _dispatcher.Stop();
        }

        public void StopCommunication(TimeSpan shutdownTimeout)
        {
            if ( _thread.Join( shutdownTimeout ))
            {
                _channel.Abort();
            }

            StopCommunication();
        }

        public void Dispose()
        {
            StopCommunication();

            _channel.Dispose();   // this method should not dispose the dispatch because we used agregation pattern: the object is passed to constructor so the may be reused after releasing the channel object.
            _dispatcher.Dispose();
        }

        public bool WaitForConnection( TimeSpan shutdownTimeout )
        {
            return _channel.WaitForConnection( shutdownTimeout );
        }

        public async Task<bool> WaitForConnectionAsync( TimeSpan shutdownTimeout )
        {
            return await Task.Run( () => _channel.WaitForConnection( shutdownTimeout ) );
        }

        private void RaiseEvent( EventArgs e )
        {
            if ( e is RTSPPacketReceivedEventArgs ) // For performance reason, let this condition statement at this position
            {
                OnPacketReceived( e as RTSPPacketReceivedEventArgs );
            }
            
            else if ( e is RTSPMessageReceivedEventArgs )
            {
                OnMessageReceived( e as RTSPMessageReceivedEventArgs );
            }

            else if (e is RTSPStreamingStatusChangedEventArgs)
            {
                OnStreamingStatusChanged(e as RTSPStreamingStatusChangedEventArgs);
            }

            else if ( e is RTSPStreamingStartedEventArgs )
            {
                OnStreamingStarted( e as RTSPStreamingStartedEventArgs );
            }
            else if ( e is RTSPStreamingStoppedEventArgs )
            {
                OnStreamingStopped( e as RTSPStreamingStoppedEventArgs );
            }
            else if ( e is RTSPStreamingSetupEventArgs )
            {
                OnStreamingSetup( e as RTSPStreamingSetupEventArgs );
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
