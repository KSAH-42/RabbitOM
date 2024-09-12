using System;
using System.Threading;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent the proxy status
    /// </summary>
    internal sealed class RtspProxyStatus
    {
        private const uint                   DefaultMaxErrors = 10;



        
        private readonly object              _lock            = new object();

        private uint                         _numberOfErrors  = 0;

        private readonly EventWaitHandle     _eventHandle     = new ManualResetEvent( false );





        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Gets the actual status
        /// </summary>
        public bool State
        {
            get => _eventHandle.TryWait( TimeSpan.Zero );
        }





        /// <summary>
        /// Initialize
        /// </summary>
        public void Initialize()
        {
            lock ( _lock )
            {
                _eventHandle.TryReset();

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
                if ( _numberOfErrors >= DefaultMaxErrors )
                {
                    _eventHandle.TryReset();

                    return false;
                }

                _eventHandle.TrySet();

                return true;
            }
        }

        /// <summary>
        /// Deactivate. This method does not reset the number of error.
        /// </summary>
        public void TurnOff()
        {
            _eventHandle.TryReset();
        }

        /// <summary>
        /// Block the calling thread until the status change
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool WaitActivation( TimeSpan timeout )
        {
            return _eventHandle.TryWait( timeout );
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

                if ( _numberOfErrors >= DefaultMaxErrors )
                {
                    _numberOfErrors = DefaultMaxErrors;

                    _eventHandle.TryReset();
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

                _eventHandle.TrySet();
            }
        }
    }
}
