using System;

namespace RabbitOM.Streaming.RtspV2.Transports
{
    // this class must handle reopen, error reporting and transmit packet to the upper layer without parsing the payload
    public class RtspReceiverService : IDisposable
    {
        private readonly IReceiver _receiver;

        public RtspReceiverService( IReceiver receiver )
        {
            _receiver = receiver ?? throw new ArgumentNullException( nameof( receiver ) );
        }

        ~RtspReceiverService()
        {
            Dispose( false );
        }

        public bool IsStarted
        {
            get => throw new NotImplementedException( "To be implemented" );
        }

        public void Start()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public void Stop()
        {
            throw new NotImplementedException( "To be implemented" );
        }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected void Dispose( bool disposing )
        {
            if ( disposing )
            {
                Stop();
            }
        }
    }
}
