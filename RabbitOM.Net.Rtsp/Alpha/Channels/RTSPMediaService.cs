using RabbitOM.Net.Rtsp.Remoting;
using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public sealed class RTSPMediaService : IDisposable
    {
        private readonly object _lock = new object();

        private readonly IRTSPEventDispatcher _dispatcher;

        private readonly IRTSPClientConfiguration _configuration;

        private readonly IRTSPConnection _connection;

        private readonly RTSPValueBag<bool> _receivingStatus;

        private readonly RTSPValueBag<bool> _setupStatus;

        private readonly RTSPValueBag<bool> _playingStatus;

        private readonly RTSPValueBag<bool> _streamingStatus;

        private readonly RTSPValueBag<string> _sessionId;





        public RTSPMediaService( IRTSPEventDispatcher dispatcher )
        {
            _dispatcher = dispatcher;

            _configuration = new RTSPClientConfiguration();
            _connection = new RTSPConnection();
            _receivingStatus = new RTSPValueBag<bool>();
            _setupStatus = new RTSPValueBag<bool>();
            _playingStatus = new RTSPValueBag<bool>();
            _sessionId = new RTSPValueBag<string>();
            _streamingStatus = new RTSPValueBag<bool>();
        }




        public object SyncRoot
            => _lock;
       
        public IRTSPClientConfiguration Configuration
            => _configuration;
        
        public IRTSPEventDispatcher Dispatcher
            => _dispatcher;

        public string SessionId
            => _sessionId.Value ?? string.Empty;

        public bool IsConnected
            => _connection.IsConnected;
       
        public bool IsOpened
            => _connection.IsOpened;
       
        public bool IsSetup
            => _setupStatus.Value;
       
        public bool IsPlaying
            => _playingStatus.Value;
        
        public bool IsStreamingStarted
            => _streamingStatus.Value;

        public bool IsReceivingPacket
            => _receivingStatus.Value;

        public bool IsDisposed
            => throw new NotImplementedException();







        public bool Open()
            => throw new NotImplementedException();
        public bool Close()
            => throw new NotImplementedException();
        // Do not dispose the dispatcher, the object is passed to the constructor
        // And it may be reused elsewhere
        public void Dispose()
            => throw new NotImplementedException();
        public void Abort()
            => throw new NotImplementedException();
        public bool Options()
            => throw new NotImplementedException();
        public bool Describe()
            => throw new NotImplementedException();
        public bool SetupAsTcp()
            => throw new NotImplementedException();
        public bool SetupAsUdp()
            => throw new NotImplementedException();
        public bool SetupAsMulticast()
            => throw new NotImplementedException();
        public bool Play()
            => throw new NotImplementedException();
        public bool TearDown()
            => throw new NotImplementedException();
        public bool KeepAliveAsOptions()
            => throw new NotImplementedException();
        public bool KeepAliveAsGetParameter()
            => throw new NotImplementedException();
        public bool KeepAliveAsSetParameter()
            => throw new NotImplementedException();
        public bool WaitForConnection(TimeSpan shutdownTimeout)
            => _connection.WaitConnectionSucceed( shutdownTimeout );

        public void UpdateReceivingStatus(bool status)
        {
            // TODO: add check + fire events

            _receivingStatus.Value = status;
        }

        public void UpdateStreamingRunningStatus(bool status)
        {
            // TODO: add check + fire events

            _receivingStatus.Value = status;
        }
    }
}
