// catch exception and returns null O-N-L-Y if the error counter has exceed at this moment exception must be THROW
// let's it hidding errors during a period, and increase the error count and change and activate a state throw exceptions at all times
// it will force the higher level implementation to close the channel and to reopen a new one and stop to continue to used a bad readers or making an unnecessary network operations
using System;

namespace RabbitOM.Streaming.RtspV2.Transports
{
    public sealed class RtspTransportWithCircuitBreaker : ITransport
    {
        private readonly ITransport _transport;
        private readonly int _maximumOfFailures;
        private readonly int _maximumOfRetriesBeforeFailure;
        private int _failureCount;
        private bool _switchState;

        public RtspTransportWithCircuitBreaker( ITransport transport )
            : this ( transport , 3 , 3 )
        {
        }

        public RtspTransportWithCircuitBreaker( ITransport transport , int maximumOfFailures , int maximumOfRetriesBeforeFailure )
        {
            _transport = transport ?? throw new ArgumentNullException( nameof( transport ) );
            _maximumOfFailures = maximumOfFailures > 0 ? maximumOfFailures : throw new ArgumentException( nameof( maximumOfFailures ) );
            _maximumOfRetriesBeforeFailure = maximumOfRetriesBeforeFailure > 0 ? maximumOfRetriesBeforeFailure : throw new ArgumentException( nameof( maximumOfRetriesBeforeFailure ) );
        }

        public int Receive( byte[] buffer , int offset , int count )
        {
            if ( _switchState )
            {
                throw new InvalidOperationException( "the transport layer is in invalid state" );
            }

            for ( var i = 0 ; i < _maximumOfRetriesBeforeFailure ; ++ i )
            {
                try
                {
                    var result = _transport.Receive( buffer , offset , count );

                    _failureCount = result < 0 ? ++ _failureCount : 0;

                    return result;
                }
                catch ( Exception exception )
                {
                    OnException( exception );
                }
            }

            _failureCount ++;

            return -1;
        }

        public void Send( byte[] buffer , int offset , int count )
        {
            if ( _switchState )
            {
                throw new InvalidOperationException( "the transport layer is in invalid state" );
            }

            try
            {
                // don't reset failure counter here
                _transport.Send( buffer , offset , count );
            }
            catch ( Exception exception )
            {
                _failureCount ++;

                OnException( exception );
            }
        }

        public void Close()
        {
            _transport.Close();
        }

        public void Dispose()
        {
            _transport.Dispose();
        }

        private void OnException( Exception exception )
        {
            _switchState = _failureCount >= _maximumOfFailures;

            if ( _switchState )
            {
                throw new Exception( "Max error has been reachs" , exception );
            }
        }
    }
}