using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent the client configuration
    /// </summary>
    public sealed class RTSPTcpClientConfiguration : RTSPClientConfiguration
    {
        /// <summary>
        /// Disable the default constructor
        /// </summary>
		private RTSPTcpClientConfiguration()
		{
		}





        /// <summary>
        /// Create the configuration
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <returns>returns an instance</returns>
        public static RTSPTcpClientConfiguration CreateConfiguration(string uri)
        {
            return new RTSPTcpClientConfiguration()
            {
                Uri = uri,
            };
        }

        /// <summary>
        /// Create the configuration
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="userName">the username</param>
        /// <param name="password">the password</param>
        /// <returns>returns an instance</returns>
        public static RTSPTcpClientConfiguration CreateConfiguration(string uri,string userName,string password)
        {
            return new RTSPTcpClientConfiguration()
            {
                Uri = uri,
                UserName = userName,
                Password = password
            };
        }

        /// <summary>
        /// Create the configuration
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="userName">the username</param>
        /// <param name="password">the password</param>
        /// <param name="keepAliveType">the keep alive type</param>
        /// <param name="mediaFormat">the media format</param>
        /// <param name="receiveTimeout">the receive timeout</param>
        /// <param name="sendTimeout">the send timeout</param>
        /// <param name="retriesInterval">the retries interval</param>
        /// <param name="keepAliveInterval">the keep alive interval</param>
        /// <returns>returns an instance</returns>
        public static RTSPTcpClientConfiguration CreateConfiguration(
            string uri, 
            string userName, 
            string password, 
            RTSPKeepAliveType keepAliveType, 
            RTSPMediaFormat mediaFormat, 
            TimeSpan receiveTimeout,
            TimeSpan sendTimeout,
            TimeSpan retriesInterval,
            TimeSpan keepAliveInterval
            )
        {
            return new RTSPTcpClientConfiguration()
            {
                Uri = uri,  
                UserName = userName,
                Password = password ,
                KeepAliveType = keepAliveType,
                MediaFormat = mediaFormat,
                ReceiveTimeout = receiveTimeout ,
                SendTimeout = sendTimeout,
                RetriesInterval = retriesInterval,
                KeepAliveInterval = keepAliveInterval,
            };
        }
    }
}
