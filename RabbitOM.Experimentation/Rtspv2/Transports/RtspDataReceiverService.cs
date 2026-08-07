using System;

namespace RabbitOM.Streaming.RtspV2.Transports
{
    public class RtspDataReceiverService : IDisposable
    {
        private readonly IDataReceiver _receiver;


        public RtspDataReceiverService( IDataReceiver receiver )
        {
            _receiver = receiver ?? throw new ArgumentNullException( nameof( receiver ) );
        }

        ~RtspDataReceiverService()
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
