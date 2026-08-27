using System;
using System.Threading;

namespace RabbitOM.Net.Rtsp.Clients
{
    using RabbitOM.Threading;

    internal sealed class RtspProxyStatus
    {
        private const uint DefaultMaxErrors = 10;


        private readonly object _lock = new object();
        private uint _numberOfErrors = 0;
        private readonly ManualResetEvent _eventHandle = new ManualResetEvent( false );


        public object SyncRoot
        {
            get => _lock;
        }

        public bool State
        {
            get => _eventHandle.TryWait( TimeSpan.Zero );
        }



        public void Initialize()
        {
            lock ( _lock )
            {
                _eventHandle.TryReset();

                _numberOfErrors = 0;
            }
        }

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

        public void TurnOff()
        {
            _eventHandle.TryReset();
        }

        public bool WaitActivation( TimeSpan timeout )
        {
            return _eventHandle.TryWait( timeout );
        }

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
