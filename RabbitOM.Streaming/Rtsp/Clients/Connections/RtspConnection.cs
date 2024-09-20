﻿using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent a proxy used to interact with a remote Rtsp server
    /// </summary>
    public sealed class RtspConnection : IRtspConnection
    {
        private readonly RtspProxy _proxy = new RtspProxy();






        /// <summary>
        /// Raise when the connection has been successfully opened
        /// </summary>
        public event EventHandler<RtspConnectionOpenedEventArgs> Opened
        {
            add    => _proxy.Opened += value;
            remove => _proxy.Opened -= value;
        }

        /// <summary>
        /// Raised when the connection has been closed
        /// </summary>
        public event EventHandler<RtspConnectionClosedEventArgs> Closed
        {
            add    => _proxy.Closed += value;
            remove => _proxy.Closed -= value;
        }

        /// <summary>
        /// Raised when a message has been sended
        /// </summary>
        public event EventHandler<RtspMessageSendedEventArgs> MessageSended
        {
            add    => _proxy.MessageSended += value;
            remove => _proxy.MessageSended -= value;
        }

        /// <summary>
        /// Raised when a message has been received
        /// </summary>
        public event EventHandler<RtspMessageReceivedEventArgs> MessageReceived
        {
            add    => _proxy.MessageReceived += value;
            remove => _proxy.MessageReceived -= value;
        }

        /// <summary>
        /// Raised when a message has been received
        /// </summary>
        public event EventHandler<RtspPacketReceivedEventArgs> PacketReceived
        {
            add    => _proxy.DataReceived += value;
            remove => _proxy.DataReceived -= value;
        }

        /// <summary>
        /// Raised when the authentication has failed
        /// </summary>
        public event EventHandler<RtspAuthenticationFailedEventArgs> AuthenticationFailed
        {
            add    => _proxy.AuthenticationFailed += value;
            remove => _proxy.AuthenticationFailed -= value;
        }

        /// <summary>
        /// Raised when an error is detected
        /// </summary>
        public event EventHandler<RtspConnectionErrorEventArgs> Error
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
        /// Gets the user name
        /// </summary>
        public string UserName
        {
            get => _proxy.UserName;
        }

        /// <summary>
        /// Gets the password
        /// </summary>
        public string Password
        {
            get => _proxy.Password;
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
        /// Check if the underlaying connection has been disposed
        /// </summary>
        public bool IsDisposed
        {
            get => _proxy.IsDisposed;
        }




        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="Exception"/>
        public void Open(string uri)
        {
            _proxy.Open( uri );
        }

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="userName">the user name</param>
        /// <param name="password">the password</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="Exception"/>
        public void Open(string uri, string userName , string password )
        {
            _proxy.Open( uri , userName , password );
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
        /// <param name="userName">the user name</param>
        /// <param name="password">the password</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryOpen( string uri , string userName , string password )
        {
            return _proxy.TryOpen( uri , userName , password );
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
        /// Configure the receive timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <exception cref="Exception"/>
        public void ConfigureReceiveTimeout( TimeSpan timeout )
        {
            _proxy.ConfigureReceiveTimeout( timeout );
        }

        /// <summary>
        /// Configure the send timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <exception cref="Exception"/>
        public void ConfigureSendTimeout( TimeSpan timeout )
        {
            _proxy.ConfigureSendTimeout( timeout );
        }

        /// <summary>
        /// Configure the receive timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryConfigureReceiveTimeout( TimeSpan timeout )
        {
            return _proxy.TryConfigureReceiveTimeout( timeout );
        }

        /// <summary>
        /// Configure the send timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryConfigureSendTimeout( TimeSpan timeout )
        {
            return _proxy.TryConfigureSendTimeout( timeout );
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
        public bool SendRequest( RtspMessageRequest request , out RtspMessageResponse response )
        {
            return _proxy.RequestManager.TrySendRequest( request , out response );
        }

        /// <summary>
        /// Wait the connection succeed
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool WaitForConnected( TimeSpan timeout )
        {
            return _proxy.WaitForConnected( timeout );
        }

        /// <summary>
        /// Call the options method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRtspInvoker Options()
        {
            return _proxy.InvocationManager.CreateOptionsInvoker();
        }

        /// <summary>
        /// Call the describe method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRtspInvoker Describe()
        {
            return _proxy.InvocationManager.CreateDescribeInvoker();
        }

        /// <summary>
        /// Call the setup method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRtspInvoker Setup()
        {
            return _proxy.InvocationManager.CreateSetupInvoker();
        }

        /// <summary>
        /// Call the play method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRtspInvoker Play()
        {
            return _proxy.InvocationManager.CreatePlayInvoker();
        }

        /// <summary>
        /// Call the pause method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRtspInvoker Pause()
        {
            return _proxy.InvocationManager.CreatePauseInvoker();
        }

        /// <summary>
        /// Call the teardown method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRtspInvoker TearDown()
        {
            return _proxy.InvocationManager.CreateTearDownInvoker();
        }

        /// <summary>
        /// Call the get parameter
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRtspInvoker GetParameter()
        {
            return _proxy.InvocationManager.CreateGetParameterInvoker();
        }

        /// <summary>
        /// Call the set parameter
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRtspInvoker SetParameter()
        {
            return _proxy.InvocationManager.CreateSetParameterInvoker();
        }

        /// <summary>
        /// Call the announce method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRtspInvoker Announce()
        {
            return _proxy.InvocationManager.CreateAnnounceInvoker();
        }

        /// <summary>
        /// Call the redirect method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRtspInvoker Redirect()
        {
            return _proxy.InvocationManager.CreateRedirectInvoker();
        }

        /// <summary>
        /// Call the record method
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRtspInvoker Record()
        {
            return _proxy.InvocationManager.CreateRecordInvoker();
        }

        /// <summary>
        /// Invoke a keep alive operation
        /// </summary>
        /// <returns>returns an invoker</returns>
        public IRtspInvoker KeepAlive()
        {
            return _proxy.InvocationManager.CreateKeepAliveInvoker();
        }

        /// <summary>
        /// Invoke a keep alive operation
        /// </summary>
        /// <param name="type">the type</param>
        /// <returns>returns an invoker</returns>
        public IRtspInvoker KeepAlive( RtspKeepAliveType type )
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
