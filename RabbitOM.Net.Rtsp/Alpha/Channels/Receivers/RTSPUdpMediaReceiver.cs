using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public sealed class RTSPUdpMediaReceiver : RTSPMediaReceiver
    {
        private readonly RTSPThread _thread;

        private readonly RTSPUdpSocket _socket;

        public RTSPUdpMediaReceiver( RTSPMediaService service )
            : base( service )
		{
            _socket = new RTSPUdpSocket();
            _thread = new RTSPThread( "RTSP - UDP Receiver" );
        }

        public override bool IsStarted 
            => _thread.IsStarted;

        public override bool Start()
            => _thread.Start( () =>
            {
                OnStreamingStarted( new RTSPStreamingStartedEventArgs() );

                TimeSpan idleTimeout = TimeSpan.Zero;

                while ( _thread.CanContinue( idleTimeout ) )
                {
                    Run( this , ref idleTimeout );
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

        private static void Run( RTSPUdpMediaReceiver receiver, ref TimeSpan idleTimeout )
        {
            if ( ! receiver._socket.IsOpened )
            {
                idleTimeout = receiver.Service.Configuration.RetriesTransportInterval;

                if ( ! receiver._socket.Open( receiver.Service.Configuration.RtpPort ) )
                {
                    // Call OnTransportError( new ReceiveTransportError() ) ?

                    return;
                }

                if ( ! receiver._socket.SetReceiveTimeout( receiver.Service.Configuration.ReceiveTransportTimeout ) )
                {
                    // Call OnTransportError( new ReceiveTransportError() ) ?

                    receiver._socket.Close();

                    return;
                }

                idleTimeout = TimeSpan.Zero;
            }
            else
            {
                var buffer = receiver._socket.Receive();

                if ( null == buffer || buffer.Length <= 0 )
                {
                    // Call OnTransportError( new ReceiveTransportError() ) ?
                    // Add in this method update streaming StreamingReceovering/StreamingInActive handler ?

                    return;
                }

                receiver.OnPacketReceived( new RTSPPacketReceivedEventArgs( buffer ) );
            }
        }
    }
}
