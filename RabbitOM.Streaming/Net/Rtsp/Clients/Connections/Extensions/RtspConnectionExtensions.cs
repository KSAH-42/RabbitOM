using System;
using System.Threading.Tasks;

namespace RabbitOM.Streaming.Net.Rtsp.Clients.Connections.Extensions
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
        /// <param name="userName">the user name</param>
        /// <param name="password">the password</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static async Task<bool> OpenAsync( this IRtspConnection connection , string uri , string userName , string password )
        {
            return await Task.Run( () => connection.TryOpen( uri , userName , password ) );
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
