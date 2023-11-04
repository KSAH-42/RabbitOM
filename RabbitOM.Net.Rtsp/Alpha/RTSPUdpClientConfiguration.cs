using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent the client configuration
    /// </summary>
    public sealed class RTSPUdpClientConfiguration : RTSPClientConfiguration
    {
        /// <summary>
        /// Represent the default port
        /// </summary>
        public const int DefaultPort = 61024;





        private int _port = DefaultPort;





        /// <summary>
        /// Disable the default constructor
        /// </summary>
		private RTSPUdpClientConfiguration()
		{
		}






        /// <summary>
        /// Gets the udp port
        /// </summary>
        public int Port
        {
            get
            {
                lock ( SyncRoot )
                {
                    return _port;
                }
            }

            private set
            {
                lock ( SyncRoot )
                {
                    RTSPClientConfigurationValidator.EnsurePortNumber(value);

                    _port = value;
                }
            }
        }








        /// <summary>
        /// Create the configuration
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="port">the port</param>
        /// <returns>returns an instance</returns>
        public static RTSPUdpClientConfiguration CreateConfiguration(string uri , int port )
        {
            return new RTSPUdpClientConfiguration()
            {
                Uri = uri,
                Port = port,
            };
        }

        /// <summary>
        /// Create the configuration
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="port">the port</param>
        /// <param name="userName">the username</param>
        /// <param name="password">the password</param>
        /// <returns>returns an instance</returns>
        public static RTSPUdpClientConfiguration CreateConfiguration(string uri,int port, string userName,string password)
        {
            return new RTSPUdpClientConfiguration()
            {
                Uri = uri,
                Port = port,
                UserName = userName,
                Password = password
            };
        }

        /// <summary>
        /// Create the configuration
        /// </summary>
        /// <param name="uri">the uri</param>
        /// <param name="port">the port</param>
        /// <param name="userName">the username</param>
        /// <param name="password">the password</param>
        /// <param name="keepAliveType">the keep alive type</param>
        /// <param name="mediaFormat">the media format</param>
        /// <param name="receiveTimeout">the receive timeout</param>
        /// <param name="sendTimeout">the send timeout</param>
        /// <param name="retriesInterval">the retries interval</param>
        /// <param name="keepAliveInterval">the keep alive interval</param>
        /// <returns>returns an instance</returns>
        public static RTSPUdpClientConfiguration CreateConfiguration(
            string uri, 
            int port ,
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
            return new RTSPUdpClientConfiguration()
            {
                Uri = uri,  
                Port = port,
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
