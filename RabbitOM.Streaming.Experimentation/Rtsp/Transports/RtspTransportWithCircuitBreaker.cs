using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports
{
    // catch exception and returns null O-N-L-Y if the error counter has now exceed at this moment exception must be RETHROW
    // let's it hidding errors during a period, and increase the error count and change it's state in error and rethrow exceptions at all times
    // and force the higher level to close the channel be cause the transport layer is in invalid state

    public sealed class RtspTransportWithCircuitBreaker : ITransport
    {
        private readonly ITransport _transport;
        private readonly int _maxFailures;
        private int _failureCount;





        public RtspTransportWithCircuitBreaker( ITransport transport )
            : this ( transport , 4 )
        {
        }

        public RtspTransportWithCircuitBreaker( ITransport transport , int maxErrors )
        {
            _transport = transport ?? throw new ArgumentNullException( nameof( transport ) );
            _maxFailures = maxErrors > 0 ? maxErrors : throw new ArgumentException( nameof( maxErrors ) );
        }






        public int Receive( byte[] buffer , int offset , int count )
        {
            if ( _failureCount >= _maxFailures )
            {
                throw new InvalidOperationException( "the transport layer is in invalid state" );
            }

            try
            {
                var result = _transport.Receive( buffer , offset , count );

                if ( result > 0 )
                {
                    _failureCount = 0;
                }

                return result;
            }
            catch ( Exception exception )
            {
                OnException( exception );
            }

            return -1;
        }

        public void Send( byte[] buffer , int offset , int count )
        {
            if ( _failureCount >= _maxFailures )
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
            if ( _failureCount >= _maxFailures )
            {
                throw new Exception( "Max error has been reachs" , exception );
            }

            _failureCount ++;
        }
    }
}