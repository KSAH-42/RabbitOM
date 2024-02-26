using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    internal class RTSPMediaClient : RTSPClient
    {
        private readonly RTSPEventDispatcher _dispatcher;
       
        private readonly RTSPMediaChannel _channel;
        
        private readonly RTSPThread _thread;





        public RTSPMediaClient()
        {
            _dispatcher = new RTSPEventDispatcher( RaiseEvent );
            _channel = new RTSPMediaChannel( _dispatcher );
            _thread = new RTSPThread( "RTSP - Client thread" );
        }



        public override object SyncRoot
        {
            get => _channel.SyncRoot;
        }

		public override RTSPPipeLineCollection PipeLines
        {
            get => _channel.PipeLines; 
        }

		public override IRTSPClientConfiguration Configuration
        {
            get => _channel.Configuration;
        }

        public override bool IsCommunicationStarted
        {
            get => _thread.IsStarted;
        }

        public override bool IsConnected
        {
            get => _channel.IsConnected;
        }

        public override bool IsStreamingStarted
        {
            get => _channel.IsPlaying;
        }

        public override bool IsReceivingPacket
        {
            get => _channel.IsReceivingPacket;
        }

        public override bool IsDisposed
        {
            get => _channel.IsDisposed;
        }





        public override bool StartCommunication()
        {
            _dispatcher.Start();
            
            return _thread.Start( () =>
            {
                _dispatcher.Dispatch( new RTSPCommunicationStartedEventArgs() );

                using ( var runner = new RTSPMediaChannelRunner( _channel ) )
                {
                    while ( _thread.CanContinue( runner.IdleTimeout ) )
                    {
                        runner.Run();
                    }
                }

                _dispatcher.Dispatch( new RTSPCommunicationStoppedEventArgs() );
            } );
        }

        public override void StopCommunication()
        {
            _thread.Stop();

            _dispatcher.Stop();
        }

        public override void StopCommunication( TimeSpan shutdownTimeout )
        {
            if ( _thread.Join( shutdownTimeout ))
            {
                _channel.Abort();
            }

            StopCommunication();
        }

        protected override void Dispose( bool disposing )
        {
            if ( ! disposing )
                return;

            StopCommunication();

            _channel.Dispose();   // this method should not dispose the dispatch because we used agregation pattern: the object is passed to constructor so the may be reused after releasing the channel object.
            _dispatcher.Dispose();
        }
    }
}
