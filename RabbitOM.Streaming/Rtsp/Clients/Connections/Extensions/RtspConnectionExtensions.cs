using System;
using System.Threading.Tasks;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections.Extensions
{
    /// <summary>
    /// Represent a connection extension class
    /// </summary>
    public static class RtspConnectionExtensions
    {
        /// <summary>
        /// Open a new connection
        /// </summary>
        /// <param name="connection">the connection</param>
        /// <param name="uri">the uri</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static async Task<bool> OpenAsync( this IRtspConnection connection , string uri )
        {
            return await Task.Run( () => connection.TryOpen( uri ) );
        }

        /// <summary>
        /// Open a new connection
        /// </summary>
        /// <param name="connection">the connection</param>
        /// <param name="uri">the uri</param>
        /// <param name="credentials">the credentials</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static async Task<bool> OpenAsync( this IRtspConnection connection , string uri , RtspCredentials credentials )
        {
            return await Task.Run( () => connection.TryOpen( uri , credentials ) );
        }

        /// <summary>
        /// Wait the connection succeed
        /// </summary>
        /// <param name="connection">the connection</param>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static async Task<bool> WaitForConnectedAsync( this IRtspConnection connection , TimeSpan timeout )
        {
            return await Task.Run( () => connection.WaitForConnected( timeout ) );
        }
    }
}
