using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent the proxy status
    /// </summary>
    internal sealed class RtspProxyStatus
    {
        private readonly object              _lock            = new object();

        private readonly uint                _maximumOfErrors = 0;

        private uint                         _numberOfErrors  = 0;

        private readonly RtspEventWaitHandle _eventHandle     = new RtspEventWaitHandle();





        /// <summary>
        /// Constructor
        /// </summary>
        public RtspProxyStatus()
            : this( 10 )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maximumOfErrors">the maximum of error</param>
        public RtspProxyStatus( uint maximumOfErrors )
        {
            if ( maximumOfErrors == uint.MinValue || maximumOfErrors == uint.MaxValue )
            {
                throw new ArgumentException( nameof( maximumOfErrors ) );
            }

            _maximumOfErrors = maximumOfErrors;
        }





        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Gets the maximum of errors
        /// </summary>
        public uint MaximumOfErrors
        {
            get => _maximumOfErrors;
        }

        /// <summary>
        /// Gets the number of errors
        /// </summary>
        public uint NumberOfErrors
        {
            get
            {
                lock ( _lock )
                {
                    return _numberOfErrors;
                }
            }
        }

        /// <summary>
        /// Gets the actual status
        /// </summary>
        public bool IsOnline
        {
            get => _eventHandle.Wait( TimeSpan.Zero );
        }





        /// <summary>
        /// Initialize
        /// </summary>
        public void Initialize()
        {
            lock ( _lock )
            {
                _eventHandle.Reset();

                _numberOfErrors = 0;
            }
        }

        /// <summary>
        /// Try to activate the status
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TurnOn()
        {
            lock ( _lock )
            {
                if ( _numberOfErrors >= _maximumOfErrors )
                {
                    _eventHandle.Reset();

                    return false;
                }

                _eventHandle.Set();

                return true;
            }
        }

        /// <summary>
        /// Deactivate. This method does not reset the number of error.
        /// </summary>
        public void TurnOff()
        {
            _eventHandle.Reset();
        }

        /// <summary>
        /// Block the calling thread until the status change
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool WaitActivation( TimeSpan timeout )
        {
            return _eventHandle.Wait( timeout );
        }

        /// <summary>
        /// Increase the error
        /// </summary>
        public void IncreaseErrors()
        {
            lock ( _lock )
            {
                if ( _numberOfErrors != uint.MaxValue )
                {
                    _numberOfErrors++;
                }

                if ( _numberOfErrors >= _maximumOfErrors )
                {
                    _numberOfErrors = _maximumOfErrors;

                    _eventHandle.Reset();
                }
            }
        }

        /// <summary>
        /// Keep alive the status
        /// </summary>
        public void KeepStatusActive()
        {
            lock ( _lock )
            {
                _numberOfErrors = 0;

                _eventHandle.Set();
            }
        }
    }
}
