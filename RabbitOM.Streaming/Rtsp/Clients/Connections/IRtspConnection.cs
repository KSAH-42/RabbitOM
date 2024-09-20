﻿using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent a proxy used to interact with a remote Rtsp server
    /// </summary>
    public interface IRtspConnection : IDisposable
    {
        /// <summary>
        /// Raise when the connection has been successfully opened
        /// </summary>
        event EventHandler<RtspConnectionOpenedEventArgs>     Opened;

        /// <summary>
        /// Raised when the connection has been closed
        /// </summary>
        event EventHandler<RtspConnectionClosedEventArgs>     Closed;

        /// <summary>
        /// Raised when a message has been sended
        /// </summary>
        event EventHandler<RtspMessageSendedEventArgs>        MessageSended;

        /// <summary>
        /// Raised when a message has been received
        /// </summary>
        event EventHandler<RtspMessageReceivedEventArgs>      MessageReceived;

        /// <summary>
        /// Raised when a message has been received
        /// </summary>
        event EventHandler<RtspPacketReceivedEventArgs>       PacketReceived;

        /// <summary>
        /// Raised when the authentication has failed
        /// </summary>
        event EventHandler<RtspAuthenticationFailedEventArgs> AuthenticationFailed;

        /// <summary>
        /// Raised when an error is detected
        /// </summary>
        event EventHandler<RtspConnectionErrorEventArgs>      Error;





        /// <summary>
        /// Gets the sync root
        /// </summary>
        object SyncRoot
        {
            get;
        }

        /// <summary>
        /// Gets the uri
        /// </summary>
        string Uri
        {
            get;
        }

        /// <summary>
        /// Gets the user name
        /// </summary>
        string UserName
        {
            get;
        }

        /// <summary>
        /// Gets the password
        /// </summary>
        string Password
        {
            get;
        }

        /// <summary>
        /// Check if the connection is still active
        /// </summary>
        bool IsConnected
        {
            get;
        }

        /// <summary>
        /// Check if the underlaying connection has been opened
        /// </summary>
        bool IsOpened
        {
            get;
        }

        /// <summary>
        /// Check if the underlaying connection has been disposed
        /// </summary>
        bool IsDisposed
        {
            get;
        }





        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        void Open(string uri);

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="userName">the user name</param>
        /// <param name="password">the password</param>
        void Open(string uri, string userName , string password );

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <returns>returns true for a success, otherwise false</returns>
        bool TryOpen( string uri );

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="userName">the user name</param>
        /// <param name="password">the password</param>
        /// <returns>returns true for a success, otherwise false</returns>
        bool TryOpen( string uri , string userName , string password );

        /// <summary>
        /// Close the connection
        /// </summary>
        void Close();

        /// <summary>
        /// Abort the connection
        /// </summary>
        void Abort();

        /// <summary>
        /// Configure the receive timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <exception cref="Exception"/>
        void ConfigureReceiveTimeout( TimeSpan timeout );

        /// <summary>
        /// Configure the send timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <exception cref="Exception"/>
        void ConfigureSendTimeout( TimeSpan timeout );

        /// <summary>
        /// Configure the receive timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        bool TryConfigureReceiveTimeout( TimeSpan timeout );

        /// <summary>
        /// Configure the send timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        bool TryConfigureSendTimeout( TimeSpan timeout );

        /// <summary>
        /// Gets the next sequence identifier
        /// </summary>
        /// <returns>returns the next message identifier value</returns>
        int GetNextSequenceId();

        /// <summary>
        /// Send a request
        /// </summary>
        /// <param name="request">the request</param>
        /// <param name="response">the response</param>
        /// <returns>returns true for a success, otherwise false</returns>
        bool SendRequest( RtspMessageRequest request , out RtspMessageResponse response );

        /// <summary>
        /// Wait the connection succeed
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        bool WaitForConnected( TimeSpan timeout );

        /// <summary>
        /// Call the options method
        /// </summary>
        /// <returns>returns an invoker</returns>
        IRtspInvoker Options();

        /// <summary>
        /// Call the describe method
        /// </summary>
        /// <returns>returns an invoker</returns>
        IRtspInvoker Describe();

        /// <summary>
        /// Call the setup method
        /// </summary>
        /// <returns>returns an invoker</returns>
        IRtspInvoker Setup();

        /// <summary>
        /// Call the play method
        /// </summary>
        /// <returns>returns an invoker</returns>
        IRtspInvoker Play();

        /// <summary>
        /// Call the pause method
        /// </summary>
        /// <returns>returns an invoker</returns>
        IRtspInvoker Pause();

        /// <summary>
        /// Call the teardown method
        /// </summary>
        /// <returns>returns an invoker</returns>
        IRtspInvoker TearDown();

        /// <summary>
        /// Call the get parameter
        /// </summary>
        /// <returns>returns an invoker</returns>
        IRtspInvoker GetParameter();

        /// <summary>
        /// Call the set parameter
        /// </summary>
        /// <returns>returns an invoker</returns>
        IRtspInvoker SetParameter();

        /// <summary>
        /// Call the announce method
        /// </summary>
        /// <returns>returns an invoker</returns>
        IRtspInvoker Announce();

        /// <summary>
        /// Call the redirect method
        /// </summary>
        /// <returns>returns an invoker</returns>
        IRtspInvoker Redirect();

        /// <summary>
        /// Call the record method
        /// </summary>
        /// <returns>returns an invoker</returns>
        IRtspInvoker Record();

        /// <summary>
        /// Invoke a keep alive operation
        /// </summary>
        /// <returns>returns an invoker</returns>
        IRtspInvoker KeepAlive();

        /// <summary>
        /// Invoke a keep alive operation
        /// </summary>
        /// <param name="type">the type</param>
        /// <returns>returns an invoker</returns>
        IRtspInvoker KeepAlive( RtspKeepAliveType type );
    }
}
