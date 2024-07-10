using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent the internal proxy settings class
    /// </summary>
    internal sealed class RtspProxySettings
    {
        private readonly object                   _lock                  = new object();

        private string                            _uri                   = string.Empty;

        private RtspCredentials                   _credentials           = RtspCredentials.Empty;

        private TimeSpan                          _receiveTimeout        = TimeSpan.Zero;

        private TimeSpan                          _sendTimeout           = TimeSpan.Zero;



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
        /// Gets / Sets the credentials
        /// </summary>
        public RtspCredentials Credentials
        {
            get
            {
                lock ( _lock )
                {
                    return _credentials;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _credentials = value ?? RtspCredentials.Empty;
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
