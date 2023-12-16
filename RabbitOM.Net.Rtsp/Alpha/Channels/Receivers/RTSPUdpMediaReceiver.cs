using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public sealed class RTSPUdpMediaReceiver : RTSPMediaReceiver
    {
        private readonly RTSPThread _thread;

        private readonly RTSPTrackInfo _trackInfo;

        private readonly RTSPUdpSocket _socket;

        private TimeSpan _idleTimeout;

        public RTSPUdpMediaReceiver( RTSPMediaService service , RTSPTrackInfo trackInfo )
            : base( service )
		{
            if ( trackInfo == null )
            {
                throw new ArgumentNullException( nameof( trackInfo ) );
            }

            _trackInfo = trackInfo;

            _socket = new RTSPUdpSocket();
            _thread = new RTSPThread( "RTSP - UDP Receiver" );
        }

        public override bool IsStarted 
            => _thread.IsStarted;

        public override bool Start()
            => _thread.Start( () =>
            {
                OnStreamingStarted( new RTSPStreamingStartedEventArgs( _trackInfo ) );

                _idleTimeout = TimeSpan.Zero;

                while ( _thread.CanContinue( _idleTimeout ) )
                {
                    Run();
                }

                OnStreamingStopped( new RTSPStreamingStoppedEventArgs() );
            });

        public override void Stop()
        {
            _socket.Close();
            _thread.Stop();
        }

        public override void Dispose()
        {
            Stop();
            _socket.Dispose();
        }

        private void Run()
        {
            if ( ! _socket.IsOpened )
            {
                _idleTimeout = Service.Configuration.RetriesTransportInterval;

                if ( ! _socket.Open( Service.Configuration.RtpPort ) )
                {
                    // Call OnTransportError( new ReceiveTransportError() ) ?

                    return;
                }

                if ( ! _socket.SetReceiveTimeout( Service.Configuration.ReceiveTransportTimeout ) )
                {
                    // Call OnTransportError( new ReceiveTransportError() ) ?

                    _socket.Close();

                    return;
                }

                _idleTimeout = TimeSpan.Zero;
            }
            else
            {
                var buffer = _socket.Receive();

                if ( null == buffer || buffer.Length <= 0 )
                {
                    // Call OnTransportError( new ReceiveTransportError() ) ?
                    // Add in this method update streaming StreamingReceovering/StreamingInActive handler ?

                    return;
                }

                OnPacketReceived( new RTSPPacketReceivedEventArgs( buffer ) );
            }
        }
    }
}
