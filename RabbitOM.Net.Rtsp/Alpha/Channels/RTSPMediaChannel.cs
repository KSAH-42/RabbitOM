using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public sealed class RTSPMediaChannel : IRTSPMediaChannel
    {
        private readonly RTSPMediaService _service;





        public RTSPMediaChannel( IRTSPEventDispatcher dispatcher )
           => _service = new RTSPMediaService( dispatcher );





        public object SyncRoot
            => _service.SyncRoot;
        
        public IRTSPClientConfiguration Configuration
            => _service.Configuration;
        
        public IRTSPEventDispatcher Dispatcher
            => _service.Dispatcher;
       
        public bool IsConnected
            => _service.IsConnected;
        
        public bool IsOpened
            => _service.IsOpened;
       
        public bool IsReceivingPacket
            => _service.IsReceivingPacket;
        
        public bool IsSetup
            => _service.IsSetup;
        
        public bool IsPlaying
            => _service.IsStreamingStarted;
        
        public bool IsDisposed
            => _service.IsDisposed;

        
        
        
        
        
        public bool Open()
            => _service.Open();

        public bool Close()
            => _service.Close();

        public void Dispose()
            => _service.Dispose();

        public void Abort()
            => _service.Abort();

        public bool Options()
            => _service.Options();
        
        public bool Describe()
            => _service.Describe();
       
        public bool Setup()
        {
            if (_service.Configuration.DeliveryMode == RTSPDeliveryMode.Udp)
            {
                return _service.SetupAsUdp();
            }

            if (_service.Configuration.DeliveryMode == RTSPDeliveryMode.Multicast)
            {
                return _service.SetupAsMulticast();
            }

            return _service.SetupAsTcp();
        }

        public bool Play()
            => _service.Play();

        public void TearDown()
            => _service.TearDown();

        public bool KeepAlive()
        {
            if ( _service.Configuration.KeepAliveType == RTSPKeepAliveType.GetParameter )
            {
                return _service.KeepAliveAsGetParameter();
            }

            if ( _service.Configuration.KeepAliveType == RTSPKeepAliveType.SetParameter )
            {
                return _service.KeepAliveAsSetParameter();
            }

            return _service.KeepAliveAsOptions();
        }

        public bool WaitForConnection(TimeSpan shutdownTimeout)
            => _service.WaitForConnection(shutdownTimeout);
    }
}
