using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public sealed class RTSPMulticastMediaReceiver : RTSPMediaReceiver
    {
        private readonly RTSPThread _thread;

        private readonly RTSPMulticastSocket _socket;

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

        private static void Run( RTSPMulticastMediaReceiver receiver , ref TimeSpan idleTimeout )
        {
            if ( ! receiver._socket.IsOpened )
            {
                idleTimeout = receiver.Service.Configuration.RetriesTransportInterval;

                if ( ! receiver._socket.Open(
                    receiver.Service.Configuration.MulticastAddress ,
                    receiver.Service.Configuration.RtpPort ,
                    receiver.Service.Configuration.TimeToLive ,
                    receiver.Service.Configuration.ReceiveTransportTimeout )
                    )
                {
                    // Call OnTransportError( new ReceiveTransportError() ) ?
                    return;
                }

                idleTimeout = TimeSpan.Zero;
            }
            else
            {
                if ( ! receiver._socket.PollReceive( receiver.Service.Configuration.ReceiveTransportTimeout ) )
                {
                    // Call OnTransportError( new ReceiveTransportError() ) ?
                    // Add in this method update streaming StreamingReceovering/StreamingInActive handler ?
                    return;
                }

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
