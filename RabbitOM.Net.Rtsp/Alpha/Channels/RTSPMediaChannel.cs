using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    internal sealed class RTSPMediaChannel : IRTSPMediaChannel
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
       
        public bool IsSetup
            => _service.IsSetup;
        
        public bool IsPlaying
            => _service.IsPlaying;

        public bool IsReceivingPacket
            => _service.IsReceivingPacket;

        public bool IsDisposed
            => _service.IsDisposed;

        
        
        
        
        
        public bool Open()
        {
            return _service.Open();
        }

        public void Close()
        {
            _service.Close();
        }

        public void Dispose()
        {
            _service.Dispose();
        }

        public void Abort()
        {
            _service.Abort();
        }

        public bool Options()
        {
            return _service.Options();
        }
        
        public bool Describe()
        {
            return _service.Describe();
        }
       
        public bool Setup()
        {
            _service.PrepareSetup();

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
        {
            return _service.Play();
        }

        public void TearDown()
        {
            _service.TearDown();
        }

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
    }
}
