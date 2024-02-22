using System;
using System.Threading.Tasks;

namespace RabbitOM.Net.Rtsp.Remoting.Extensions
{
    /// <summary>
    /// Represent a connection extension class
    /// </summary>
    public static class RTSPConnectionExtensions
    {
        /// <summary>
        /// Open a new connection
        /// </summary>
        /// <param name="connection">the connection</param>
        /// <param name="uri">the uri</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static async Task<bool> OpenAsync( this IRTSPConnection connection , string uri )
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
        public static async Task<bool> OpenAsync( this IRTSPConnection connection , string uri , RTSPCredentials credentials )
        {
            return await Task.Run( () => connection.TryOpen( uri , credentials ) );
        }

        /// <summary>
        /// Wait the connection succeed
        /// </summary>
        /// <param name="connection">the connection</param>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static async Task<bool> WaitForConnectionAsync( this IRTSPConnection connection , TimeSpan timeout )
        {
            return await Task.Run( () => connection.WaitForConnection( timeout ) );
        }
    }
}
