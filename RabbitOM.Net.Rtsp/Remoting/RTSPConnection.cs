using System;

namespace RabbitOM.Net.Rtsp.Remoting
{
    /// <summary>
    /// Represent a proxy used to interact with a remote RTSP server
    /// </summary>
    public sealed class RTSPConnection : IRTSPConnection
    {
        private readonly RTSPProxy _proxy = new RTSPProxy();






        /// <summary>
        /// Raise when the connection has been successfully opened
        /// </summary>
        public event EventHandler<RTSPConnectionOpenedEventArgs> Opened
        {
            add    => _proxy.Opened += value;
            remove => _proxy.Opened -= value;
        }

        /// <summary>
        /// Raised when the connection has been closed
        /// </summary>
        public event EventHandler<RTSPConnectionClosedEventArgs> Closed
        {
            add    => _proxy.Closed += value;
            remove => _proxy.Closed -= value;
        }

        /// <summary>
        /// Raised when a message has been sended
        /// </summary>
        public event EventHandler<RTSPMessageSendedEventArgs> MessageSended
        {
            add    => _proxy.MessageSended += value;
            remove => _proxy.MessageSended -= value;
        }

        /// <summary>
        /// Raised when a message has been received
        /// </summary>
        public event EventHandler<RTSPMessageReceivedEventArgs> MessageReceived
        {
            add    => _proxy.MessageReceived += value;
            remove => _proxy.MessageReceived -= value;
        }

        /// <summary>
        /// Raised when a message has been received
        /// </summary>
        public event EventHandler<RTSPPacketReceivedEventArgs> PacketReceived
        {
            add    => _proxy.DataReceived += value;
            remove => _proxy.DataReceived -= value;
        }

        /// <summary>
        /// Raised when the authentication has failed
        /// </summary>
        public event EventHandler<RTSPAuthenticationFailedEventArgs> AuthenticationFailed
        {
            add    => _proxy.AuthenticationFailed += value;
            remove => _proxy.AuthenticationFailed -= value;
        }

        /// <summary>
        /// Raised when an error is detected
        /// </summary>
        public event EventHandler<RTSPConnectionErrorEventArgs> Error
        {
            add    => _proxy.Error += value;
            remove => _proxy.Error -= value;
        }






        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _proxy.SyncRoot;
        }

        /// <summary>
        /// Gets the uri
        /// </summary>
        public string Uri
        {
            get => _proxy.Uri;
        }

        /// <summary>
        /// Gets the credentials
        /// </summary>
        public RTSPCredentials Credentials
        {
            get => _proxy.Credentials;
        }

        /// <summary>
        /// Check if the connection is still active
        /// </summary>
        public bool IsConnected
        {
            get => _proxy.IsConnected;
        }

        /// <summary>
        /// Check if the underlaying connection has been opened
        /// </summary>
        public bool IsOpened
        {
            get => _proxy.IsOpened;
        }





        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="Exception"/>
        public void Open(string uri)
        {
            _proxy.Open(uri);
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="credentials">the credentials</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="Exception"/>
        public void Open(string uri, RTSPCredentials credentials)
        {
            _proxy.Open(uri,credentials);
        }

        /// <summary>
        /// Try to open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryOpen( string uri )
        {
            return _proxy.TryOpen( uri );
        }

        /// <summary>
        /// Try to open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="credentials">the credentials</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryOpen( string uri , RTSPCredentials credentials )
        {
            return _proxy.TryOpen( uri , credentials );
        }

        /// <summary>
        /// Close the connection
        /// </summary>
        public void Close()
        {
            _proxy.Close();
        }

        /// <summary>
        /// Abort the connection
        /// </summary>
        public void Abort()
        {
            _proxy.Abort();
        }

        /// <summary>
        /// Configure the timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <exception cref="Exception"/>
        public void ConfigureTimeouts(TimeSpan timeout)
        {
            _proxy.ConfigureTimeouts(timeout);
        }

        /// <summary>
        /// Configure the timeout
        /// </summary>
        /// <param name="receiveTimeout">the receive timeout</param>
        /// <param name="sendTimeout">the send timeout</param>
        /// <exception cref="Exception"/>
        public void ConfigureTimeouts(TimeSpan receiveTimeout, TimeSpan sendTimeout)
        {
            _proxy.ConfigureTimeouts(receiveTimeout, sendTimeout);
        }

        /// <summary>
        /// Configure the timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryConfigureTimeouts( TimeSpan timeout )
        {
            return _proxy.TryConfigureTimeouts( timeout );
        }

        /// <summary>
        /// Configure the timeout
        /// </summary>
        /// <param name="receiveTimeout">the receive timeout</param>
        /// <param name="sendTimeout">the send timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryConfigureTimeouts( TimeSpan receiveTimeout , TimeSpan sendTimeout )
        {
            return _proxy.TryConfigureTimeouts( receiveTimeout , sendTimeout );
        }

        /// <summary>
        /// Gets the next sequence identifier
        /// </summary>
        /// <returns></returns>
        public int GetNextSequenceId()
        {
            return _proxy.GetNextSequenceId();
        }

        /// <summary>
        /// Try to send a request
        /// </summary>
        /// <param name="request">the request</param>
        /// <param name="response">the response</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool SendRequest( RTSPMessageRequest request , out RTSPMessageResponse response )
        {
            return _proxy.RequestManager.TrySendRequest( request , out response );
        }

        /// <summary>
        /// Wait the connection succeed
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool WaitConnectionSucceed( TimeSpan timeout )
        {
            return _proxy.WaitConnectionSucceed( timeout );
        }

        /// <summary>
        /// Call the options method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRTSPInvoker Options()
        {
            return _proxy.InvocationManager.CreateOptionsInvoker();
        }

        /// <summary>
        /// Call the describe method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRTSPInvoker Describe()
        {
            return _proxy.InvocationManager.CreateDescribeInvoker();
        }

        /// <summary>
        /// Call the setup method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRTSPInvoker Setup()
        {
            return _proxy.InvocationManager.CreateSetupInvoker();
        }

        /// <summary>
        /// Call the play method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRTSPInvoker Play()
        {
            return _proxy.InvocationManager.CreatePlayInvoker();
        }

        /// <summary>
        /// Call the pause method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRTSPInvoker Pause()
        {
            return _proxy.InvocationManager.CreatePauseInvoker();
        }

        /// <summary>
        /// Call the teardown method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRTSPInvoker TearDown()
        {
            return _proxy.InvocationManager.CreateTearDownInvoker();
        }

        /// <summary>
        /// Call the get parameter
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRTSPInvoker GetParameter()
        {
            return _proxy.InvocationManager.CreateGetParameterInvoker();
        }

        /// <summary>
        /// Call the set parameter
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRTSPInvoker SetParameter()
        {
            return _proxy.InvocationManager.CreateSetParameterInvoker();
        }

        /// <summary>
        /// Call the announce method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRTSPInvoker Announce()
        {
            return _proxy.InvocationManager.CreateAnnounceInvoker();
        }

        /// <summary>
        /// Call the redirect method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRTSPInvoker Redirect()
        {
            return _proxy.InvocationManager.CreateRedirectInvoker();
        }

        /// <summary>
        /// Call the record method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRTSPInvoker Record()
        {
            return _proxy.InvocationManager.CreateRecordInvoker();
        }

        /// <summary>
        /// Invoke a keep alive operation
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRTSPInvoker KeepAlive()
        {
            return _proxy.InvocationManager.CreateKeepAliveInvoker();
        }

        /// <summary>
        /// Invoke a keep alive operation
        /// </summary>
        /// <param name="type">the type</param>
        /// <returns>returns an invoker</returns>
        public IRTSPInvoker KeepAlive( RTSPKeepAliveType type )
        {
            return _proxy.InvocationManager.CreateKeepAliveInvoker(type);
        }

        /// <summary>
        /// Release internal resources
        /// </summary>
        public void Dispose()
        {
            _proxy.Dispose();
        }
    }
}
