using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    // TODO: the lock need to removed, it is now un necessary

    /// <summary>
    /// Represent the client configuration
    /// </summary>
    public abstract class RTSPClientConfiguration
    {
        /// <summary>
        /// Represent the default receive timeout
        /// </summary>
        public readonly static TimeSpan   DefaultReceiveTimeout     = TimeSpan.FromSeconds(15);

        /// <summary>
        /// Represent the default send timeout
        /// </summary>
        public readonly static TimeSpan   DefaultSendTimeout        = TimeSpan.FromSeconds(15);

        /// <summary>
        /// Represent the default keep alive interval
        /// </summary>
        public readonly static TimeSpan   DefaultKeepAliveInterval  = TimeSpan.FromSeconds(15);

        /// <summary>
        /// Represent the default retries interval
        /// </summary>
        public readonly static TimeSpan   DefaultRetriesInterval    = TimeSpan.FromSeconds(15);



                                                                                            

        private readonly object     _lock              = new object();

        private string              _uri               = string.Empty;

        private string              _userName          = string.Empty;

        private string              _password          = string.Empty;
        
        private TimeSpan            _receiveTimeout    = DefaultReceiveTimeout;

        private TimeSpan            _sendTimeout       = DefaultSendTimeout;

        private TimeSpan            _retriesInterval   = DefaultRetriesInterval ;

        private TimeSpan            _keepAliveInterval = DefaultKeepAliveInterval;

        private RTSPKeepAliveType   _keepAliveType     = RTSPKeepAliveType.Options;

        private RTSPMediaFormat _mediaFormat       = RTSPMediaFormat.Video;






       
        /// <summary>
        /// Gets the sync root
        /// </summary>
        protected object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Gets the uri
        /// </summary>
        public string Uri
        {
            get
            {
                lock ( _lock )
                {
                    return _uri;
                }
            }

            protected set
            {
                lock ( _lock )
                {
                    RTSPClientConfigurationValidator.EnsureUriWellFormed( value );

                    _uri = value;
                }
            }
        }

        /// <summary>
        /// Gets the user name
        /// </summary>
        public string UserName
        {
            get
            {
                lock ( _lock )
                {
                    return _userName;
                }
            }

            protected set
            {
                lock ( _lock )
                {
                    _userName = value ?? string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the password
        /// </summary>
        public string Password
        {
            get
            {
                lock (_lock)
                {
                    return _password;
                }
            }

            protected set
            {
                lock (_lock)
                {
                    _password = value ?? string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the receive timeout
        /// </summary>
        public TimeSpan ReceiveTimeout
        {
            get
            {
                lock (_lock)
                {
                    return _receiveTimeout;
                }
            }

            protected set
            {
                lock (_lock)
                {
                    RTSPClientConfigurationValidator.EnsureTimeoutValue(value);

                    _receiveTimeout = value;
                }
            }
        }

        /// <summary>
        /// Gets the send timeout
        /// </summary>
        public TimeSpan SendTimeout
        {
            get
            {
                lock (_lock)
                {
                    return _sendTimeout;
                }
            }

            protected set
            {
                lock (_lock)
                {
                    RTSPClientConfigurationValidator.EnsureTimeoutValue(value);

                    _sendTimeout = value;
                }
            }
        }

		/// <summary>
		/// Gets the retries interval
		/// </summary>
		public TimeSpan RetriesInterval
        {
            get
            {
                lock (_lock)
                {
                    return _retriesInterval;
                }
            }

            protected set
            {
                lock (_lock)
                {
                    RTSPClientConfigurationValidator.EnsureTimeoutValue(value);

                    _retriesInterval = value;
                }
            }
        }

        /// <summary>
        /// Gets the keep alive interval
        /// </summary>
        public TimeSpan KeepAliveInterval
        {
            get
            {
                lock (_lock)
                {
                    return _keepAliveInterval;
                }
            }

            protected set
            {
                lock (_lock)
                {
                    RTSPClientConfigurationValidator.EnsureTimeoutValue(value);

                    _keepAliveInterval = value;
                }
            }
        }

        /// <summary>
        /// Gets the keep alive mode
        /// </summary>
        public RTSPKeepAliveType KeepAliveType
        {
            get
            {
                lock (_lock)
                {
                    return _keepAliveType;
                }
            }

            protected set
            {
                lock (_lock)
                {
                    _keepAliveType = value;
                }
            }
        }

        /// <summary>
        /// Gets the media format type
        /// </summary>
        public RTSPMediaFormat MediaFormat
        {
            get
            {
                lock (_lock)
                {
                    return _mediaFormat;
                }
            }

            protected set
            {
                lock (_lock)
                {
                    _mediaFormat = value;
                }
            }
        }
    }
}
