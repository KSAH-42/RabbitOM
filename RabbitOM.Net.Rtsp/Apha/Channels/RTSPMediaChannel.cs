using RabbitOM.Net.Rtsp.Remoting;
using System;

namespace RabbitOM.Net.Rtsp.Apha
{
    public sealed class RTSPMediaChannel : IRTSPMediaChannel
    {
        private readonly IRTSPClientDispatcher _dispatcher;

        private readonly IRTSPConnection _connection;





        public RTSPMediaChannel( IRTSPClientDispatcher dispatcher )
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException( nameof( dispatcher ) );
            _connection = new RTSPConnection();
        }





        public object SyncRoot
        {
            get => _connection.SyncRoot;
        }
        
        public IRTSPClientConfiguration Configuration
            => throw new NotImplementedException();
        
        public IRTSPClientDispatcher Dispatcher
            => throw new NotImplementedException();
        
        public bool IsConnected
            => throw new NotImplementedException();
        
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
