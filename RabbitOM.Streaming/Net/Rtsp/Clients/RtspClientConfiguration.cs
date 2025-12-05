using System;

namespace RabbitOM.Streaming.Net.Rtsp.Clients
{
    /// <summary>
    /// Represent the client configuration
    /// </summary>
    public sealed class RtspClientConfiguration : IRtspClientConfiguration
    {
        /// <summary>
        /// Represent the default receive timeout
        /// </summary>
        public readonly static TimeSpan   DefaultReceiveTimeout     = TimeSpan.FromSeconds( 15 );

        /// <summary>
        /// Represent the default send timeout
        /// </summary>
        public readonly static TimeSpan   DefaultSendTimeout        = TimeSpan.FromSeconds( 15 );

        /// <summary>
        /// Represent the default keep alive interval
        /// </summary>
        public readonly static TimeSpan   DefaultKeepAliveInterval  = TimeSpan.FromSeconds( 15 );

        /// <summary>
        /// Represent the default retries interval
        /// </summary>
        public readonly static TimeSpan   DefaultRetriesInterval    = TimeSpan.FromSeconds( 15 );

        /// <summary>
        /// Represent the default port
        /// </summary>
        public readonly static int        DefaultPort               = 61024;

        /// <summary>
        /// Represent the default TTL
        /// </summary>
        public readonly static byte       DefaultTTL                = 5;







        private readonly object      _lock              = new object();

        private string               _uri               = string.Empty;

        private string               _userName          = string.Empty;

        private string               _password          = string.Empty;
        
        private TimeSpan             _receiveTimeout    = TimeSpan.Zero;

        private TimeSpan             _sendTimeout       = TimeSpan.Zero;

        private RtspKeepAliveType    _keepAliveType     = RtspKeepAliveType.Options;

        private TimeSpan             _retriesInterval   = DefaultKeepAliveInterval;

        private TimeSpan             _keepAliveInteval  = DefaultKeepAliveInterval;
       
        private RtspMediaFormat      _mediaFormat       = RtspMediaFormat.Video;

        private RtspDeliveryMode     _deliveryMode      = RtspDeliveryMode.Tcp;

        private int                  _rtpPort           = 0;

        private string               _multicastAddress  = string.Empty;

        private byte                 _timeToLive        = 0;
       
        




        /// <summary>
        /// Initialize an new instance
        /// </summary>
        public RtspClientConfiguration()
        {
            ToDefault();
        }
        




        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Gets / Sets the uri
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

            set
            {
                lock ( _lock )
                {
                    _uri = value ?? string.Empty;
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
                    return _userName;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _userName = value ?? string.Empty;
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
                    return _password;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _password = value ?? string.Empty;
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
                    _receiveTimeout = value != TimeSpan.Zero ? value : DefaultReceiveTimeout;
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
                    _sendTimeout = value != TimeSpan.Zero ? value : DefaultSendTimeout;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the keep alive mode
        /// </summary>
        public RtspKeepAliveType KeepAliveType
        {
            get
            {
                lock ( _lock )
                {
                    return _keepAliveType;
                }
            }
            
            set
            {
                lock ( _lock )
                {
                    _keepAliveType = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the retries interval
        /// </summary>
        public TimeSpan RetriesInterval
        {
            get
            {
                lock ( SyncRoot )
                {
                    return _retriesInterval;
                }
            }

            set
            {
                lock ( SyncRoot )
                {
                    _retriesInterval = value != TimeSpan.Zero ? value : DefaultRetriesInterval;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the keep alive interval
        /// </summary>
        public TimeSpan KeepAliveInterval
        {
            get
            {
                lock ( SyncRoot )
                {
                    return _keepAliveInteval;
                }
            }

            set
            {
                lock ( SyncRoot )
                {
                    _keepAliveInteval = value != TimeSpan.Zero ? value : DefaultKeepAliveInterval;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the media format type
        /// </summary>
        public RtspMediaFormat MediaFormat
        {
            get
            {
                lock ( _lock )
                {
                    return _mediaFormat;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _mediaFormat = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the packet delivery mode
        /// </summary>
        public RtspDeliveryMode DeliveryMode
        {
            get
            {
                lock ( _lock )
                {
                    return _deliveryMode;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _deliveryMode = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the port
        /// </summary>
        public int RtpPort
        {
            get
            {
                lock ( _lock )
                {
                    return _rtpPort;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _rtpPort = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the multicast address
        /// </summary>
        public string MulticastAddress
        {
            get
            {
                lock ( _lock )
                {
                    return _multicastAddress;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _multicastAddress = value ?? string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the TTL
        /// </summary>
        public byte TimeToLive
        {
            get
            {
                lock ( _lock )
                {
                    return _timeToLive;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _timeToLive = value;
                }
            }
        }









        /// <summary>
        /// Apply the default parameters
        /// </summary>
        public void ToDefault()
        {
            lock ( _lock )
            {
                _uri              = string.Empty;
                _userName         = string.Empty;
                _password         = string.Empty;
                _receiveTimeout   = DefaultReceiveTimeout;
                _sendTimeout      = DefaultSendTimeout;
                _keepAliveType    = RtspKeepAliveType.Options;
                _retriesInterval  = DefaultRetriesInterval;
                _keepAliveInteval = DefaultKeepAliveInterval;
                _mediaFormat      = RtspMediaFormat.Video;
                _deliveryMode     = RtspDeliveryMode.Tcp;
                _rtpPort          = DefaultPort + 1;
                _timeToLive       = DefaultTTL;
                _multicastAddress = string.Empty;
            }
        }
    }
}
