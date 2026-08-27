using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    using RabbitOM.Threading;

    internal sealed class RtspClientSessionDataReceiver
    {
        private readonly BackgroundWorker _thread;
        private readonly RtspSession _session;
        private readonly IDataReceiver _receiver;

        public RtspClientSessionDataReceiver( RtspSession session , IDataReceiver receiver )
        {
            _session = session ?? throw new ArgumentNullException( nameof( session ) );
            _receiver = receiver ?? throw new ArgumentNullException( nameof( receiver ) );
            _thread = new BackgroundWorker( "Rtsp - Transport session receiver" );
        }

        public bool IsStarted
        {
            get => _thread.IsStarted;
        }

        public bool Start()
        {
            return _thread.Start( () =>
            {
                var timeout = TimeSpan.Zero;

                while ( _thread.CanContinue( timeout ) )
                {
                    if ( ! _receiver.IsOpened )
                    {
                        timeout = _receiver.TryOpen() ? TimeSpan.Zero : TimeSpan.FromSeconds( 5 );
                    }
                    else
                    {
                        if ( _receiver.TryReceive( out var buffer ) )
                        {
                            OnDataReceived( buffer );
                        }
                    }
                }
            } );
        }

        public void Stop()
        {
            _receiver.Close();
            _thread.Stop();
        }

        private void OnDataReceived( byte[] data )
        {
            _session.Dispatcher.DispatchEvent( new RtspPacketReceivedEventArgs( new RtspPacket( data ) ) );
        }
    }
}
