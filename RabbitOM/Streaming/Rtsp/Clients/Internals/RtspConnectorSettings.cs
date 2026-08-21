using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    internal sealed class RtspConnectorSettings
    {
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds( 15 );


        private readonly object _lock = new object();
        private string _uri;
        private TimeSpan _receiveTimeout;
        private TimeSpan _sendTimeout;
        private string _userName;
        private string _password;


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


        public static TimeSpan GetTimeoutOrDefault( TimeSpan timeout  )
        {
            return timeout.Ticks > 0 ? timeout : DefaultTimeout;
        }
    }
}
