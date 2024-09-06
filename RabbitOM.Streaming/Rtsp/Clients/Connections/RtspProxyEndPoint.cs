using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent the internal proxy endoint class
    /// </summary>
    public sealed class RtspProxyEndPoint
    {
        private readonly object _lock = new object();

        private string _uri;

        private TimeSpan _receiveTimeout;

        private TimeSpan _sendTimeout;

        private RtspCredentials _credentials;






        /// <summary>
        /// Gets / Sets the uri
        /// </summary>
        public string Uri
        {
            get
            {
                lock ( _lock )
                {
                    return _uri ?? string.Empty;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _uri = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the credentials
        /// </summary>
        public RtspCredentials Credentials
        {
            get
            {
                lock ( _lock )
                {
                    return _credentials ?? RtspCredentials.Empty;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _credentials = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the receive timeout
        /// </summary>
        public TimeSpan ReceiveTimeout
        {
            get
            {
                lock ( _lock )
                {
                    return _receiveTimeout;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _receiveTimeout = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the send timeout
        /// </summary>
        public TimeSpan SendTimeout
        {
            get
            {
                lock ( _lock )
                {
                    return _sendTimeout;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _sendTimeout = value;
                }
            }
        }
    }
}
