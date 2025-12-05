using System;

namespace RabbitOM.Streaming.Net.Rtsp.Clients
{
    /// <summary>
    /// Represent the client
    /// </summary>
    public interface IRtspClient : IRtspClientEvents , IDisposable
    {
        /// <summary>
        /// Gets the sync root
        /// </summary>
        object SyncRoot
        {
            get;
        }

        /// <summary>
        /// Gets the configuration
        /// </summary>
        IRtspClientConfiguration Configuration
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
        /// Check if the communication is actually stopping
        /// </summary>
        bool IsCommunicationStopping
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
        /// Just wait until the communication is active
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>return true for a success, otherwise false</returns>
        bool WaitForConnected( TimeSpan timeout );
    }
}
