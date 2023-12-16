using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public sealed class RTSPMulticastMediaReceiver : RTSPMediaReceiver
    {
        private readonly RTSPThread _thread;

        private readonly RTSPMulticastSocket _socket;

        private TimeSpan _idleTimeout;

        public RTSPMulticastMediaReceiver( RTSPMediaService service )
            : base( service )
		{
            _socket = new RTSPMulticastSocket();
            _thread = new RTSPThread( "RTSP - Multicast Receiver" );
		}

        public override bool IsStarted 
            => _thread.IsStarted;

        public override bool Start()
            => _thread.Start( () =>
            {
                OnStreamingStarted( new RTSPStreamingStartedEventArgs() );

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

                if ( ! _socket.Open( 
                    Service.Configuration.MulticastAddress ,
                    Service.Configuration.RtpPort , 
                    Service.Configuration.TimeToLive , 
                    Service.Configuration.ReceiveTransportTimeout )
                    )
                {
                    // Call OnTransportError( new ReceiveTransportError() ) ?
                    return;
                }

                _idleTimeout = TimeSpan.Zero;
            }
            else
            {
                if ( ! _socket.PollReceive( Service.Configuration.ReceiveTransportTimeout ) )
                {
                    // Call OnTransportError( new ReceiveTransportError() ) ?
                    // Add in this method update streaming StreamingReceovering/StreamingInActive handler ?
                    return;
                }

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
