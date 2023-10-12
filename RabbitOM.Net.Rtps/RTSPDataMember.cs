using System;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent a data field object
    /// </summary>
    /// <typeparam name="TValue">the type of the value</typeparam>
    public sealed class RTSPDataMember<TValue>
    {
        private readonly object _lock   = new object();

        private TValue          _value  = default;





        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPDataMember()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">the value</param>
        public RTSPDataMember( TValue value )
        {
            _value = value;
        }





        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public TValue Value
        {
            get
            {
                lock ( _lock )
                {
                    return _value;
                }
            }

            set
            {
                lock ( _lock )
                {
                    _value = value;
                }
            }
        }
    }
}
