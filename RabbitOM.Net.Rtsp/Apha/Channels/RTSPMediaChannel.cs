using RabbitOM.Net.Rtsp.Remoting;
using System;

namespace RabbitOM.Net.Rtsp.Apha
{
    public sealed class RTSPMediaChannel : IRTSPMediaChannel
    {
        private readonly IRTSPConnection _connection;

        private readonly IRTSPClientConfiguration _configuration;

        private readonly IRTSPClientDispatcher _dispatcher;





		public RTSPMediaChannel( IRTSPClientDispatcher dispatcher )
            : this ( dispatcher , new RTSPClientConfiguration() , new RTSPConnection() )
        {
		}

        public RTSPMediaChannel( IRTSPClientDispatcher dispatcher , IRTSPClientConfiguration configuration , IRTSPConnection connection )
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
        
        public IRTSPClientDispatcher Dispatcher
        {
            get => _dispatcher;
        }

        public bool IsConnected
        {
            get => _connection.IsOpened;
        }
        
        public bool IsReceivingPacket
            => throw new NotImplementedException();
        
        public bool IsStreamingStarted
            => throw new NotImplementedException();
        
        public bool IsDisposed
            => throw new NotImplementedException();





        public bool Connect()
            => throw new NotImplementedException();
        
        public bool Disconnect()
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
