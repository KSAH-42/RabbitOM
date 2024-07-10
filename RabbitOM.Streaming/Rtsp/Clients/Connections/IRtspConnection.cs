using System;

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
        /// Gets the credentials
        /// </summary>
        RtspCredentials Credentials
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
        /// <param name="credentials">the credentials</param>
        void Open(string uri, RtspCredentials credentials);

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
        /// <param name="credentials">the credentials</param>
        /// <returns>returns true for a success, otherwise false</returns>
        bool TryOpen( string uri , RtspCredentials credentials );

        /// <summary>
        /// Close the connection
        /// </summary>
        void Close();

        /// <summary>
        /// Abort the connection
        /// </summary>
        void Abort();

        /// <summary>
        /// Configure the timeout
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        bool TryConfigureTimeouts( TimeSpan timeout );

        /// <summary>
        /// Configure the timeout
        /// </summary>
        /// <param name="receiveTimeout">the receive timeout</param>
        /// <param name="sendTimeout">the send timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        bool TryConfigureTimeouts( TimeSpan receiveTimeout , TimeSpan sendTimeout );

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
        bool WaitForConnection( TimeSpan timeout );

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
