using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    /// <summary>
    /// Represent the client configuration
    /// </summary>
    public class RTSPClientConfiguration : IRTSPClientConfiguration
    {
        /// <summary>
        /// Represent the default receive timeout
        /// </summary>
        public readonly static TimeSpan   DefaultReceiveTimeout     = TimeSpan.FromSeconds( 15 );

        /// <summary>
        /// Represent the default send timeout
        /// </summary>
        public readonly static TimeSpan   DefaultSendTimeout        = TimeSpan.FromSeconds( 15 );






        private readonly object      _lock              = new object();

        private string               _uri               = string.Empty;

        private string               _userName          = string.Empty;

        private string               _password          = string.Empty;
        
        private TimeSpan             _receiveTimeout    = TimeSpan.Zero;

        private TimeSpan             _sendTimeout       = TimeSpan.Zero;

        private RTSPKeepAliveType    _keepAliveType     = RTSPKeepAliveType.Options;

        




        /// <summary>
        /// Initialize an new instance
        /// </summary>
        public RTSPClientConfiguration()
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

        /// <summary>
        /// Gets / Sets the keep alive mode
        /// </summary>
        public RTSPKeepAliveType KeepAliveType
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
        /// Apply the default parameters
        /// </summary>
        public virtual void ToDefault()
        {
            lock ( _lock )
            {
                _uri              = string.Empty;
                _userName         = string.Empty;
                _password         = string.Empty;
                _receiveTimeout   = DefaultReceiveTimeout;
                _sendTimeout      = DefaultSendTimeout;
                _keepAliveType    = RTSPKeepAliveType.Options;
            }
        }
    }
}
