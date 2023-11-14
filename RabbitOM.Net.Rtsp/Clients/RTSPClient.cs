using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    /// <summary>
    /// Represent the client
    /// </summary>
    public class RTSPClient : IRTSPClient
    {
        private readonly RTSPClientSession _session = new RTSPClientSession();

        private readonly RTSPThread        _thread  = new RTSPThread( "RTSP - client thread" );



        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPClient()
        {
        }



        /// <summary>
        /// Raised when the communication has been started
        /// </summary>
        public event EventHandler<RTSPClientCommunicationStartedEventArgs> CommunicationStarted
        {
            add    => _session.Dispatcher.CommunicationStarted += value;
            remove => _session.Dispatcher.CommunicationStarted -= value;
        }

        /// <summary>
        /// Raised when the communication has been stopped
        /// </summary>
        public event EventHandler<RTSPClientCommunicationStoppedEventArgs> CommunicationStopped
        {
            add    => _session.Dispatcher.CommunicationStopped += value;
            remove => _session.Dispatcher.CommunicationStopped -= value;
        }

        /// <summary>
        /// Raised when the client is connected
        /// </summary>
        public event EventHandler<RTSPClientConnectedEventArgs> Connected
        {
            add    => _session.Dispatcher.Connected += value;
            remove => _session.Dispatcher.Connected -= value;
        }

        /// <summary>
        /// Raised when the client is disconnected
        /// </summary>
        public event EventHandler<RTSPClientDisconnectedEventArgs> Disconnected
        {
            add    => _session.Dispatcher.Disconnected += value;
            remove => _session.Dispatcher.Disconnected -= value;
        }

        /// <summary>
        /// Raise when an data has been received
        /// </summary>
        public event EventHandler<RTSPPacketReceivedEventArgs> PacketReceived
        {
            add    => _session.Dispatcher.PacketReceived += value;
            remove => _session.Dispatcher.PacketReceived -= value;
        }

        /// <summary>
        /// Raise when an error occurs
        /// </summary>
        public event EventHandler<RTSPClientErrorEventArgs> Error
        {
            add    => _session.Dispatcher.Error += value;
            remove => _session.Dispatcher.Error -= value;
        }
        


        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _session.SyncRoot;
        }

        /// <summary>
        /// Gets the configuration
        /// </summary>
        public IRTSPClientConfiguration Configuration
        {
            get => _session.Configuration;
        }

        /// <summary>
        /// Gets the options
        /// </summary>
        public RTSPClientConfigurationOptions Options
        {
            get => _session.Options;
        }

        /// <summary>
        /// Check if the client is connected
        /// </summary>
        public bool IsConnected
        {
            get => _session.IsConnected;
        }

        /// <summary>
        /// Check if the communication has been started
        /// </summary>
        public bool IsCommunicationStarted
        {
            get => _thread.IsStarted;
        }

        


        /// <summary>
        /// Start the communication
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool StartCommunication()
        {
            return _thread.Start( () =>
            {
                _session.Dispatcher.Run();
                _session.Dispatcher.DispatchEvent( new RTSPClientCommunicationStartedEventArgs() );

                using ( var host = new RTSPClientSessionHost( _session ) )
                {
                    while( _thread.CanContinue( host.IdleTimeout ) )
                    {
                        host.Run();
                    }
                }

                _session.Dispatcher.DispatchEvent( new RTSPClientCommunicationStoppedEventArgs() );
                _session.Dispatcher.Terminate();
            } );
        }

        /// <summary>
        /// Stop the communication
        /// </summary>
        public void StopCommunication()
        {
            _thread.Stop();
        }

        /// <summary>
        /// Stop the communication
        /// </summary>
        /// <param name="shutdownTimeout">the shutdown timeout</param>
        public void StopCommunication(TimeSpan shutdownTimeout)
		{
            if ( ! _thread.Join( shutdownTimeout ) )
			{
                _session.Abort();
            }

            _thread.Stop();
        }
    }
}
