using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the internal proxy endoint class
    /// </summary>
    public sealed class RtspEndPoint
    {
        private readonly object _lock = new object();

        private string _uri;

        private TimeSpan _receiveTimeout;

        private TimeSpan _sendTimeout;

        private string _userName;

        private string _password;






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
        /// Gets / Sets the user name
        /// </summary>
        public string UserName
        {
            get
            {
                lock ( _lock )
                {
                    return _userName ?? string.Empty;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _userName = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the password
        /// </summary>
        public string Password
        {
            get
            {
                lock ( _lock )
                {
                    return _password ?? string.Empty;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _password = value;
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
