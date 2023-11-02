using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    /// <summary>
    /// Represent the client configuration bag
    /// </summary>
    /// <typeparam name="TConfiguration">the type type of client configuration class</typeparam>
    internal sealed class RTSPClientConfigurationBag<TConfiguration> where TConfiguration : RTSPClientConfiguration
    {
        private readonly object _lock;

        private TConfiguration _configuration;




        /// <summary>
        /// Initialize the current instance
        /// </summary>         
        /// <param name="configuration">the configuration</param>
        /// <exception cref="ArgumentNullException"/>
		public RTSPClientConfigurationBag( TConfiguration configuration )
		{
            if ( configuration == null )
            {
                throw new ArgumentNullException( nameof( configuration ) );
            }

            _lock = new object();
            _configuration = configuration;
        }




        /// <summary>
        /// Gets the current instance
        /// </summary>
        /// <exception cref="InvalidOperationException"/>
        public TConfiguration Current
        {
            get
            {
                lock ( _lock )
                {
                    return _configuration ?? throw new InvalidOperationException("the configuration member can not be null");
                }
            }
        }




        /// <summary>
        /// Set the configuration
        /// </summary>
        /// <param name="configuration">the configuration</param>
        /// <exception cref="ArgumentNullException"/>
        public void Change( TConfiguration configuration )
        {
            if ( configuration == null )
            {
                throw new ArgumentNullException( nameof( configuration ) );
            }

            lock ( _lock )
            {
                _configuration = configuration;
            }
        }

        /// <summary>
        /// Set the configuration
        /// </summary>
        /// <param name="configuration">the configuration</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryChange(TConfiguration configuration)
        {
            if ( configuration == null )
            {
                return false;
            }

            lock ( _lock )
            {
                _configuration = configuration;

                return true;
            }
        }

        /// <summary>
        /// Gets the property value
        /// </summary>
        /// <typeparam name="TValue">the type of the value</typeparam>
        /// <param name="selector">the selector</param>
        /// <returns>returns a value</returns>
        public TValue GetProperty<TValue>( Func<TConfiguration,TValue> selector )
        {
            if ( selector == null )
            {
                throw new ArgumentNullException( nameof( selector ) );
            }

            lock ( _lock )
            {
                return selector( _configuration ?? throw new InvalidOperationException("the configuration member can not be null") );
            }
        }
    }
}
