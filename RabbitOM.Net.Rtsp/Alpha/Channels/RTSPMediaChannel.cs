using RabbitOM.Net.Rtsp.Remoting;
using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public sealed class RTSPMediaChannel : IRTSPMediaChannel
    {
        private readonly IRTSPConnection _connection;

        private readonly IRTSPClientConfiguration _configuration;

        private readonly IRTSPEventDispatcher _dispatcher;





		public RTSPMediaChannel( IRTSPEventDispatcher dispatcher )
            : this ( dispatcher , new RTSPClientConfiguration() , new RTSPConnection() )
        {
		}

        public RTSPMediaChannel( IRTSPEventDispatcher dispatcher , IRTSPClientConfiguration configuration , IRTSPConnection connection )
        {
            _connection = connection ?? throw new ArgumentNullException( nameof( connection ) );
            _configuration = configuration ?? throw new ArgumentNullException( nameof( configuration ) );
            _dispatcher = dispatcher ?? throw new ArgumentNullException( nameof( dispatcher ) );
        }





        public object SyncRoot
        {
            get => _connection.SyncRoot;
        }
        
        public IRTSPClientConfiguration Configuration
        {
            get => _configuration;
        }
        
        public IRTSPEventDispatcher Dispatcher
        {
            get => _dispatcher;
        }

        public bool IsOpened
        {
            get => _connection.IsOpened;
        }

        public bool IsConnected
        {
            get => _connection.IsConnected;
        }

        public bool IsReceivingPacket
            => throw new NotImplementedException();
        
        public bool IsStreamingStarted
            => throw new NotImplementedException();
        
        public bool IsDisposed
            => throw new NotImplementedException();





        public bool Open()
            => throw new NotImplementedException();
        
        public bool Close()
            => throw new NotImplementedException();
        
        public void Dispose()
            => throw new NotImplementedException();
        
        public void Abort()
            => throw new NotImplementedException();
        
        public bool StartStreaming()
            => throw new NotImplementedException();
        
        public bool StopStreaming()
            => throw new NotImplementedException();
        
        public bool KeepAlive()
            => throw new NotImplementedException();
        
        public bool WaitForConnection(TimeSpan shutdownTimeout)
            => throw new NotImplementedException();





        private void OnConnected( RTSPConnectedEventArgs e )
        {
            _dispatcher.Dispatch( e );
        }

        private void OnDisconnected( RTSPDisconnectedEventArgs e )
        {
            _dispatcher.Dispatch( e );
        }

        private void OnMessageReceived( RTSPMessageReceivedEventArgs e )
        {
            _dispatcher.Dispatch( e );
        }

        private void OnError( RTSPErrorEventArgs e )
        {
            _dispatcher.Dispatch( e );
        }
    }
}
