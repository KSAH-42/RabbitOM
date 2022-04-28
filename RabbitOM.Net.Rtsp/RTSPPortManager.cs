using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a RTSP port manager
    /// </summary>
    public sealed class RTSPPortManager
    {
        private readonly static RTSPPortManager s_default = new RTSPPortManager(54995, 65000);
                            


        private readonly object _lock           = new object();

        private readonly int    _minimumPort    = 0;

        private readonly int    _maximumPort    = 0;

        private int             _currentPort    = 0;




        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="minimumPort">the minimum port</param>
        /// <param name="maximumPort">the maximum port</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        private RTSPPortManager(int minimumPort, int maximumPort)
        {
            if ( minimumPort >= maximumPort )
			{
                throw new ArgumentOutOfRangeException(nameof(minimumPort));
			}

            _minimumPort = minimumPort;
            _maximumPort = maximumPort;
            _currentPort = minimumPort;
        }




        /// <summary>
        /// Represent the default port manager
        /// </summary>
        public static RTSPPortManager Default
		{
            get => s_default;
		}

        /// <summary>
        /// Gets the minimum port value
        /// </summary>
        public int MinimumPort
        {
            get => _minimumPort;
        }

        /// <summary>
        /// Gets the maximum port value
        /// </summary>
        public int MaximumPort
        {
            get => _maximumPort;
        }

        /// <summary>
        /// Gets the current port value
        /// </summary>
        public int CurrentPort
		{
            get
			{
                lock ( _lock )
				{
                    return _currentPort;
				}
			}
        }




        /// <summary>
        /// Select an used port
        /// </summary>
        /// <param name="sourceId">the source identifier</param>
        /// <returns>return a value</returns>
        /// <exception cref="NotImplementedException"/>
        public int AcquirePort( Guid sourceId )
		{
            throw new NotImplementedException();
		}

        /// <summary>
        /// Release the port used by 
        /// </summary>
        /// <param name="sourceId">the source identifier</param>
        /// <exception cref="NotImplementedException"/>
        public void ReleasePort( Guid sourceId )
		{
            throw new NotImplementedException();
		}

        /// <summary>
        /// Change the current port value
        /// </summary>
        /// <returns>returns the new port value</returns>
        public int SelectPort()
		{
            lock ( _lock )
			{
                _currentPort++;

                if ( _currentPort > _maximumPort )
				{
                    _currentPort = _minimumPort;
				}

                return _currentPort;
			}
		}

        /// <summary>
        /// Set the current port to the minimum port value
        /// </summary>
        public void Reset()
		{
            lock ( _lock )
			{
                _currentPort = _minimumPort;
			}
		}
    }
}
