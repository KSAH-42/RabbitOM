using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    using RabbitOM.Threading;

    public sealed class RtspClient : IRtspClient
    {
        private readonly RtspClientSession _session;
        private readonly BackgroundWorker _thread;



        public RtspClient()
        {
            _session = new RtspClientSession( this );
            _thread = new BackgroundWorker("Rtsp - client thread");
        }

        ~RtspClient()
        {
            Dispose( false );
        }



        public event EventHandler<RtspClientCommunicationStartedEventArgs> CommunicationStarted
        {
            add    => _session.Dispatcher.CommunicationStarted += value;
            remove => _session.Dispatcher.CommunicationStarted -= value;
        }

        public event EventHandler<RtspClientCommunicationStoppedEventArgs> CommunicationStopped
        {
            add    => _session.Dispatcher.CommunicationStopped += value;
            remove => _session.Dispatcher.CommunicationStopped -= value;
        }

        public event EventHandler<RtspClientConnectedEventArgs> Connected
        {
            add    => _session.Dispatcher.Connected += value;
            remove => _session.Dispatcher.Connected -= value;
        }

        public event EventHandler<RtspClientDisconnectedEventArgs> Disconnected
        {
            add    => _session.Dispatcher.Disconnected += value;
            remove => _session.Dispatcher.Disconnected -= value;
        }

        public event EventHandler<RtspPacketReceivedEventArgs> PacketReceived
        {
            add    => _session.Dispatcher.PacketReceived += value;
            remove => _session.Dispatcher.PacketReceived -= value;
        }

        public event EventHandler<RtspClientErrorEventArgs> Error
        {
            add    => _session.Dispatcher.Error += value;
            remove => _session.Dispatcher.Error -= value;
        }




        public object SyncRoot
        {
            get => _session.SyncRoot;
        }

        public IRtspClientConfiguration Configuration
        {
            get => _session.Configuration;
        }

        public bool IsConnected
        {
            get => _session.IsConnected;
        }

        public bool IsCommunicationStarted
        {
            get => _thread.IsStarted;
        }

        public bool IsCommunicationStopping
        {
            get => _thread.IsStopping;
        }




        public bool StartCommunication()
        {
            return _thread.Start( () =>
            {
                _session.Dispatcher.Run();
                _session.Dispatcher.DispatchEvent( new RtspClientCommunicationStartedEventArgs() );

                using ( var host = new RtspClientSessionHost( _session ) )
                {
                    while( _thread.CanContinue( host.IdleTimeout ) )
                    {
                        host.Run();
                    }
                }

                _session.Dispatcher.DispatchEvent( new RtspClientCommunicationStoppedEventArgs() );
                _session.Dispatcher.Terminate();
            } );
        }

        public void StopCommunication()
        {
            _thread.Stop();
        }

        public void StopCommunication(TimeSpan shutdownTimeout)
        {
            if ( ! _thread.Stop( shutdownTimeout ) )
            {
                _session.Abort();
            }

            _thread.Stop();
        }

        public bool WaitForConnected( TimeSpan timeout )
        {
            return _session.WaitForOnline( timeout );
        }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        private void Dispose( bool disposing )
        {
            if ( disposing )
            {
                StopCommunication();
                _session.Dispose();
            }
        }
    }
}
