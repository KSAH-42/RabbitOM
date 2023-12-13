using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public sealed class RTSPClientConfiguration : IRTSPClientConfiguration
    {
        private readonly object _lock = new object();

        
        private string _uri = string.Empty;

        private string _userName = string.Empty;

        private string _password = string.Empty;

        private TimeSpan _receiveTimeout;

        private TimeSpan _sendTimeout;

        private TimeSpan _keepAliveInterval;

        private TimeSpan _retriesInterval;

        private TimeSpan _receiveTransportTimeout;

        private TimeSpan _retriesTransportInterval;

        private string _multicastAddress = string.Empty;

        private int _rtpPort;

        private byte _timeToLive;

        private RTSPMediaFormat _mediaFormat;
        
        private RTSPKeepAliveType _keepAliveType;
        
        private RTSPDeliveryMode _deliveryMode;
        
        private readonly RTSPHeaderCollection _optionsHeaders = new RTSPHeaderCollection();
        
        private readonly RTSPHeaderCollection _describeHeaders = new RTSPHeaderCollection();
        
        private readonly RTSPHeaderCollection _setupHeaders = new RTSPHeaderCollection();
        
        private readonly RTSPHeaderCollection _playHeaders = new RTSPHeaderCollection();
        
        private readonly RTSPHeaderCollection _tearDownHeaders = new RTSPHeaderCollection();
        
        private readonly RTSPHeaderCollection _keepAliveHeaders = new RTSPHeaderCollection();





        public object SyncRoot
        {
            get => _lock;
        }

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

        public TimeSpan ReceiveTimeout
        {
            get
            {
                lock (_lock)
                {
                    return _receiveTimeout;
                }
            }

            set
            {
                lock (_lock)
                {
                    _receiveTimeout = value;
                }
            }
        }

        public TimeSpan ReceiveTransportTimeout
        {
            get
            {
                lock (_lock)
                {
                    return _receiveTransportTimeout;
                }
            }

            set
            {
                lock (_lock)
                {
                    _receiveTransportTimeout = value;
                }
            }
        }

        public TimeSpan KeepAliveInterval
{
            get
            {
                lock ( _lock )
                {
                    return _keepAliveInterval;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _keepAliveInterval = value;
                }
            }
        }

        public TimeSpan RetriesInterval
        {
            get
            {
                lock ( _lock )
                {
                    return _retriesInterval;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _retriesInterval = value;
                }
            }
        }

        public TimeSpan RetriesTransportInterval
        {
            get
            {
                lock ( _lock )
                {
                    return _retriesTransportInterval;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _retriesTransportInterval = value;
                }
            }
        }

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
                    _rtpPort = value > 0 ? value : 0;
                }
            }
        }

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

        public RTSPMediaFormat MediaFormat
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

        public RTSPDeliveryMode DeliveryMode
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

        public RTSPHeaderCollection OptionsHeaders
        {
            get => _optionsHeaders;
        }

        public RTSPHeaderCollection DescribeHeaders
        {
            get => _describeHeaders;
        }

        public RTSPHeaderCollection SetupHeaders
        {
            get => _setupHeaders;
        }

        public RTSPHeaderCollection PlayHeaders
        {
            get => _playHeaders;
        }

        public RTSPHeaderCollection TearDownHeaders
        {
            get => _tearDownHeaders;
        }

        public RTSPHeaderCollection KeepAliveHeaders
        {
            get => _keepAliveHeaders;
        }
    }
}
