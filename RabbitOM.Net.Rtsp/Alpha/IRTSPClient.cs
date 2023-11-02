using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent the client
    /// </summary>
    public interface IRTSPClient : IDisposable
    {
        /// <summary>
        /// Raised when the communication has been started
        /// </summary>
        event EventHandler<RTSPClientCommunicationStartedEventArgs> CommunicationStarted;

        /// <summary>
        /// Raised when the communication has been stopped
        /// </summary>
        event EventHandler<RTSPClientCommunicationStoppedEventArgs> CommunicationStopped;

        /// <summary>
        /// Raised when the client is connected
        /// </summary>
        event EventHandler<RTSPClientConnectedEventArgs> Connected;

        /// <summary>
        /// Raised when the client is disconnected
        /// </summary>
        event EventHandler<RTSPClientDisconnectedEventArgs> Disconnected;

        /// <summary>
        /// Raise when an data has been received
        /// </summary>
        event EventHandler<RTSPPacketReceivedEventArgs> PacketReceived;

        /// <summary>
        /// Raise when an error occurs
        /// </summary>
        event EventHandler<RTSPClientErrorEventArgs> Error;





        /// <summary>
        /// Gets the sync root
        /// </summary>
        object SyncRoot
        {
            get;
        }

        /// <summary>
        /// Check if the client is connected
        /// </summary>
        bool IsConnected
        {
            get;
        }

        /// <summary>
        /// Check if the communication has been started
        /// </summary>
        bool IsCommunicationStarted
        {
            get;
        }



        /// <summary>
        /// Start the communication
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        bool StartCommunication();

        /// <summary>
        /// Stop the communication
        /// </summary>
        void StopCommunication();

        /// <summary>
        /// Stop the communication
        /// </summary>
        /// <param name="shutdownTimeout">the shutdown timeout</param>
        void StopCommunication( TimeSpan shutdownTimeout );

        /// <summary>
        /// This method will block the call thread until the client has establish the connection
        /// </summary>
        /// <param name="timeout">the time to wait in milliseconds</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <remarks>
        ///     <para>This method will returns false in case the communication is not established.</para>
        ///     <para>And it will returns true when the communication is connected or already connected.</para>
        /// </remarks>
        bool WaitForConnection(int timeout);

        /// <summary>
        /// This method will block the call thread until the client has establish the connection
        /// </summary>
        /// <param name="timeout">the time to wait</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <remarks>
        ///     <para>This method will returns false in case the communication is not established.</para>
        ///     <para>And it will returns true when the communication is connected or already connected.</para>
        /// </remarks>
        bool WaitForConnection( TimeSpan timeout );
    }
}
