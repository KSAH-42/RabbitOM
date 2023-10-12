using System;

namespace RabbitOM.Net.Rtps.Clients
{
    /// <summary>
    /// Represent the client
    /// </summary>
    public interface IRTSPClient : IRTSPClientEvents
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
        IRTSPClientConfiguration Configuration
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
    }
}
