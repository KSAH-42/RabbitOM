using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    /// <summary>
    /// Represent the client options
    /// </summary>
    public sealed class RTSPClientConfigurationOptions
    {
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
        public const int                  DefaultPort               = 61024;

        /// <summary>
        /// Represent the default TTL
        /// </summary>
        public const byte                 DefaultTTL                = 5;





        private readonly object      _lock                  = new object();

        private TimeSpan             _retriesInterval       = DefaultKeepAliveInterval;

        private TimeSpan             _keepAliveInteval      = DefaultKeepAliveInterval;
       
        private RTSPMediaFormatType  _mediaFormat           = RTSPMediaFormatType.Video;

        private RTSPDeliveryMode     _deliveryMode          = RTSPDeliveryMode.Tcp;

        private int                  _unicastPort           = 0;

        private int                  _multicastPort         = 0;

        private byte                 _multicastTTL          = 0;

        private string               _multicastAddress      = string.Empty;
        





        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _lock;
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
                    _retriesInterval = value;
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
                    _keepAliveInteval = value;
                }
            }
        }   
        
        /// <summary>
        /// Gets / Sets the media format type
        /// </summary>
        public RTSPMediaFormatType MediaFormat
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

        /// <summary>
        /// Gets / Sets the unicast port
        /// </summary>
        public int UnicastPort
        {
            get
            {
                lock ( _lock )
                {
                    return _unicastPort;
                }
            }
            
            set
            {
                lock ( _lock )
                {
                    _unicastPort = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the multicast port
        /// </summary>
        public int MulticastPort
        {
            get
            {
                lock ( _lock )
                {
                    return _multicastPort;
                }
            }
            
            set
            {
                lock ( _lock )
                {
                    _multicastPort = value;
                }
            }
        }

        /// <summary>
        /// Gets / Sets the multicast TTL
        /// </summary>
        public byte MulticastTTL
        {
            get
            {
                lock ( _lock )
                {
                    return _multicastTTL;
                }
            }
            
            set
            {
                lock ( _lock )
                {
                    _multicastTTL = value;
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
        /// Apply the default parameters
        /// </summary>
        public void ToDefault()
        {
            lock ( SyncRoot )
            {
                _retriesInterval   = DefaultRetriesInterval;
                _keepAliveInteval  = DefaultKeepAliveInterval;
                _mediaFormat       = RTSPMediaFormatType.Video;
                _deliveryMode      = RTSPDeliveryMode.Tcp;
                _unicastPort       = DefaultPort;
                _multicastPort     = DefaultPort + 1;
                _multicastAddress  = string.Empty;
                _multicastTTL      = DefaultTTL;
            }
        }
    }
}
