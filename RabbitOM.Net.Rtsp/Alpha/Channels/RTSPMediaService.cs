using RabbitOM.Net.Rtsp.Remoting;
using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public sealed class RTSPMediaService : IDisposable
    {
        private readonly IRTSPEventDispatcher _dispatcher;

        public RTSPMediaService( IRTSPEventDispatcher dispatcher )
        {
            _dispatcher = dispatcher;
        }

        public object SyncRoot
            => throw new NotImplementedException();
        public IRTSPClientConfiguration Configuration
            => throw new NotImplementedException();
        public IRTSPEventDispatcher Dispatcher
            => throw new NotImplementedException();
        public bool IsConnected
            => throw new NotImplementedException();
        public bool IsOpened
            => throw new NotImplementedException();
        public bool IsReceivingPacket
            => throw new NotImplementedException();
        public bool IsStreamingStarted
            => throw new NotImplementedException();
        public bool IsPlaying
            => throw new NotImplementedException();
        public bool IsDisposed
            => throw new NotImplementedException();
        public string SessionId
            => throw new NotImplementedException();

        public bool Open()
            => throw new NotImplementedException();
        public bool Close()
            => throw new NotImplementedException();
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
            => throw new NotImplementedException();
        public void SetStreamingStatus(bool status)
            => throw new NotImplementedException();
        public void SetReceivingStatus(bool status)
            => throw new NotImplementedException();
    }
}
