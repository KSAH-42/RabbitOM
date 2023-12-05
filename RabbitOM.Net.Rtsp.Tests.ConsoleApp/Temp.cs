using System;

namespace RabbitOM.Net.Rtsp.Beta
{
    public enum RTSPStreamingStatus
    {
        Active , Inactive 
    }
    public class RTSPCommunicationStartedEventArgs : EventArgs
    {
    }
    public class RTSPCommunicationStoppedEventArgs : EventArgs
    {
    }
    public class RTSPConnectedEventArgs : EventArgs
    {
    }
    public class RTSPDisconnectedEventArgs : EventArgs
    {
    }
    public class RTSPStreamingStartedEventArgs : EventArgs
    {
    }
    public class RTSPStreamingStatusChangedEventArgs : EventArgs
    {
        public RTSPStreamingStatus Status { get; set; }
    }
    public class RTSPStreamingStoppedEventArgs : EventArgs
    {
    }
    public class RTSPPacketReceivedEventArgs : EventArgs
    {
    }
    public class RTSPErrorEventArgs : EventArgs
    {
    }
    public class RTSPTransportErrorEventArgs : RTSPErrorEventArgs
    {
    }
}

namespace RabbitOM.Net.Rtsp.Beta
{
    public sealed class RTSPClientConfiguration
    {
        public object SyncRoot { get; } = new object();
        public string Uri { get;set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public TimeSpan ReceiveTimeout { get; set; }
        public TimeSpan WriteTimeout { get;set; }
        public TimeSpan PingInterval { get; set; }
        public TimeSpan RetriesDelay { get; set; }
        public TimeSpan ReceiveTransportTimeout { get; set; }
        public TimeSpan RetriesTransportTimeout { get; set; }
        public string MulticastAddress { get; set; }
        public int RtpPort { get; set; }
        public byte TimeToLive { get; set; }
        public bool AutoStartStreaming { get; set; }
        public RTSPMediaFormat MediaFormat { get; set; }
        public RTSPKeepAliveType KeepAliveType { get; set; }
        public RTSPDeliveryMode DeliveryMode { get; set; }
        public RTSPHeaderCollection OptionsHeaders { get; } = new RTSPHeaderCollection();
        public RTSPHeaderCollection DescribeHeaders { get; } = new RTSPHeaderCollection();
        public RTSPHeaderCollection SetupHeaders { get; } = new RTSPHeaderCollection();
        public RTSPHeaderCollection PlayHeaders { get; } = new RTSPHeaderCollection();
        public RTSPHeaderCollection TearDownHeaders { get; } = new RTSPHeaderCollection();
        public RTSPHeaderCollection PingHeaders { get; } = new RTSPHeaderCollection();
    }

    public interface IRTSPClient : IDisposable
    {
        event EventHandler<RTSPCommunicationStartedEventArgs> CommunicationStarted;
        event EventHandler<RTSPCommunicationStoppedEventArgs> CommunicationStopped;
        event EventHandler<RTSPConnectedEventArgs> Connected;
        event EventHandler<RTSPDisconnectedEventArgs> Disconnected;
        event EventHandler<RTSPStreamingStartedEventArgs> StreamingStarted;
        event EventHandler<RTSPStreamingStatusChangedEventArgs> StreamingStatusChanged;
        event EventHandler<RTSPStreamingStoppedEventArgs> StreamingStopped;
        event EventHandler<RTSPPacketReceivedEventArgs> PacketReceived;
        event EventHandler<RTSPErrorEventArgs> Error;

        object SyncRoot
        {
            get;
        }

        RTSPClientConfiguration Configuration
        {
            get;
        }
        
        bool IsCommunicationStarted
        {
            get;
        }

        bool IsConnected
        {
            get;
        }

        bool IsStreamingStarted
        {
            get;
        }

        bool IsReceivingPacket
        {
            get;
        }

        bool StartCommunication();
        void StopCommunication();
        void StopCommunication(TimeSpan shutdownTimeout);
        bool WaitForConnection();
        bool WaitForConnection(TimeSpan timeout);
        bool StartStreaming();
        void StopStreaming();
        void Dispatch(Action action);
    }

    public abstract class RTSPClient : IRTSPClient
    {
        public event EventHandler<RTSPCommunicationStartedEventArgs> CommunicationStarted;
        public event EventHandler<RTSPCommunicationStoppedEventArgs> CommunicationStopped;
        public event EventHandler<RTSPConnectedEventArgs> Connected;
        public event EventHandler<RTSPDisconnectedEventArgs> Disconnected;
        public event EventHandler<RTSPStreamingStartedEventArgs> StreamingStarted;
        public event EventHandler<RTSPStreamingStoppedEventArgs> StreamingStopped;
        public event EventHandler<RTSPStreamingStatusChangedEventArgs> StreamingStatusChanged;
        public event EventHandler<RTSPPacketReceivedEventArgs> PacketReceived;
        public event EventHandler<RTSPErrorEventArgs> Error;


        public abstract object SyncRoot
        {
            get;
        }

        public abstract RTSPClientConfiguration Configuration
        {
            get;
        }

        public abstract bool IsCommunicationStarted
        {
            get;
        }

        public abstract bool IsConnected
        {
            get;
        }

        public abstract bool IsStreamingStarted
        {
            get;
        }

        public abstract bool IsReceivingPacket
        {
            get;
        }

        public abstract bool IsDisposed
        {
            get;
        }

        public abstract bool StartCommunication();
        public abstract void StopCommunication();
        public abstract void StopCommunication(TimeSpan shutdownTimeout);
        public abstract bool WaitForConnection();
        public abstract bool WaitForConnection(TimeSpan timeout);
        public abstract bool StartStreaming();
        public abstract void StopStreaming();
        public abstract void Dispatch(Action action);
        public abstract void Dispose();

        protected virtual void OnCommunicationStarted(RTSPCommunicationStartedEventArgs e)
        {
            CommunicationStarted?.TryInvoke(this, e);
        }

        protected virtual void OnCommunicationStopped(RTSPCommunicationStoppedEventArgs e)
        {
            CommunicationStopped?.TryInvoke(this, e);
        }

        protected virtual void OnConnected(RTSPConnectedEventArgs e)
        {
            Connected?.TryInvoke(this, e);
        }

        protected virtual void OnDisconnected(RTSPDisconnectedEventArgs e)
        {
            Disconnected?.TryInvoke(this, e);
        }

        protected virtual void OnStreamingStarted(RTSPStreamingStartedEventArgs e)
        {
            StreamingStarted?.TryInvoke(this, e);
        }

        protected virtual void OnStreamingStopped(RTSPStreamingStoppedEventArgs e)
        {
            StreamingStopped?.TryInvoke(this, e);
        }

        protected virtual void OnStreamingStatusChanged(RTSPStreamingStatusChangedEventArgs e)
        {
            StreamingStatusChanged?.TryInvoke(this, e);
        }

        protected virtual void OnPacketReceived(RTSPPacketReceivedEventArgs e)
        {
            PacketReceived?.TryInvoke(this, e);
        }

        protected virtual void OnError(RTSPErrorEventArgs e)
        {
            Error?.TryInvoke(this, e);
        }
    }
}

namespace RabbitOM.Net.Rtsp.Beta
{
    public abstract class RTSPClientConfigurer
    {
        public string Uri { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool PreventIfStarted { get; set; }

        public abstract void Configure();
        public abstract bool TryConfigure();
    }

    public sealed class RTSPClientTcpConfigurer : RTSPClientConfigurer
    {
        public override void Configure() => throw new NotImplementedException();
        public override bool TryConfigure() => throw new NotImplementedException();
    }

    public sealed class RTSPClientUdpConfigurer : RTSPClientConfigurer
    {
        public string IPAddress { get; set; }
        public int RtpPort {get; set; }
        
        public override void Configure() => throw new NotImplementedException();
        public override bool TryConfigure() => throw new NotImplementedException();
    }

    public sealed class RTSPClientMulticastConfigurer : RTSPClientConfigurer
    {
        public string IPAddress {get;set; }
        public int RtpPort { get; set; }
        public byte TimeToLive { get; set; }

        public override void Configure() => throw new NotImplementedException();
        public override bool TryConfigure() => throw new NotImplementedException();
    }
}

namespace RabbitOM.Net.Rtsp.Beta
{
    public abstract class RTSPClientCommandManager : IDisposable
    {
        public abstract void Dispatch(Action action);
        public abstract void Dispose();
    }

    public abstract class RTSPClientEventManager : IDisposable
    {
        public abstract void PublishEvent(EventArgs e);
        public abstract void RegisterHandler(EventHandler handler);
        public abstract void UnRegisterHandler(EventHandler handler);
        public abstract void Dispose();
        public abstract bool TryPublishEvent(EventArgs e);
    }
}

namespace RabbitOM.Net.Rtsp.Beta
{
    public abstract class RTSPClientChannel : IDisposable
    {
        public abstract object SyncRoot {get; }
        public abstract RTSPClientConfiguration Configuration { get; }
        public abstract bool IsStreamingStarted { get; }
        public abstract bool IsReceivingPacket {get; }
        public abstract bool HasErrors { get; }
        public abstract bool IsOpened { get; }

        public abstract bool Open();
        public abstract void Close();
        public abstract void Abort();
        public abstract bool StartStreaming();
        public abstract void StopStreaming();
        public abstract bool Ping();
        public abstract void WaitForConnection( TimeSpan timeout);
        public abstract void Dispose();
    }

    public sealed class RTSPClientChannelRunner : IDisposable
    {
        private readonly RTSPClientChannel _channel;
        
		public RTSPClientChannelRunner(RTSPClientChannel channel, RTSPClientEventManager eventManager)
		{
            _channel = channel;
        }

        public TimeSpan IdleTimeout {get; private set; }

        public void Run()
        {
            if ( ! _channel.IsOpened )
            {
                IdleTimeout = _channel.Configuration.RetriesDelay;

                if ( ! _channel.Open() )
                {
                    return;
                }
            }
            else
            {
                if ( _channel.Configuration.AutoStartStreaming && ! _channel.IsStreamingStarted )
                {
                    _channel.StartStreaming();
                }
                else
                {
                    _channel.Ping();
                }

                if ( ! _channel.HasErrors )
                {
                    IdleTimeout = _channel.Configuration.PingInterval;
                    return;
                }

                if (_channel.IsStreamingStarted)
                {
                    _channel.StopStreaming();
                }

                _channel.Close();

                IdleTimeout = _channel.Configuration.RetriesDelay;
            }
        }

        public void Dispose()
        {
            if (_channel.IsOpened )
            {
                if ( _channel.IsStreamingStarted )
                {
                    _channel.StopStreaming();
                }

                _channel.Close();
            }
        }
    }

    public abstract class RTSPStreamingSession : IDisposable
    {
        private readonly RTSPClientEventManager _eventManager;  // must not be accessible directly to the child, in order to avoid to raise irrelevant eventargs

        protected RTSPStreamingSession(RTSPClientEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public abstract bool IsRunning { get; }
        public abstract bool Setup();
        public abstract bool StartStreaming();
        public abstract void StopStreaming();
        public abstract void Dispose();

        protected void OnStreamingStarted(RTSPStreamingStartedEventArgs e) 
        {
            _eventManager.PublishEvent( e );
        }

        protected void OnStreamingStopped(RTSPStreamingStoppedEventArgs e)
        {
            _eventManager.TryPublishEvent(e);
        }

        protected void OnStreamingStatusChanged(RTSPStreamingStatusChangedEventArgs e)
        {
            _eventManager.TryPublishEvent(e);
        }

        protected void OnPacketReceived(RTSPPacketReceivedEventArgs e )
        {
            _eventManager.TryPublishEvent(e);
        }

        protected void OnTransportError(RTSPTransportErrorEventArgs e)
        {
            _eventManager.TryPublishEvent(e);
        }
    }

    public sealed class RTSPInterleavedStreamingSession : RTSPStreamingSession
    {
        private readonly RTSPClientChannel _channel;

        public RTSPInterleavedStreamingSession(RTSPClientChannel channel, RTSPClientEventManager eventManager)
            : base(eventManager)
		{
            _channel = channel;
		}

        public override bool IsRunning { get; }
        public override bool Setup() => false;
        public override bool StartStreaming() => false;
        public override void StopStreaming() { }
        public override void Dispose() { }
    }

    public sealed class RTSPUdpStreamingSession : RTSPStreamingSession
    {
        private readonly RTSPClientChannel _channel;

        public RTSPUdpStreamingSession(RTSPClientChannel channel, RTSPClientEventManager eventManager)
            : base(eventManager)
		{
            _channel = channel;
		}

        public override bool IsRunning { get; }
        public override bool Setup() => throw new NotImplementedException();
        public override bool StartStreaming() => throw new NotImplementedException();
        public override void StopStreaming() { throw new NotImplementedException(); }
        public override void Dispose() { }
    }

    public sealed class RTSPMulticastStreamingSession : RTSPStreamingSession
    {
        private readonly RTSPClientChannel _channel;

        public RTSPMulticastStreamingSession(RTSPClientChannel channel, RTSPClientEventManager eventManager)
            : base(eventManager)
		{
            _channel = channel;
		}

        public override bool IsRunning { get; }
        public override bool Setup() => throw new NotImplementedException();
        public override bool StartStreaming() => throw new NotImplementedException();
        public override void StopStreaming() { throw new NotImplementedException(); }
        public override void Dispose() { }
    }

    public sealed class RTSPStreamingSessionFactory
    {
        private readonly RTSPClientChannel _channel;
        private readonly RTSPClientEventManager _eventManager;

        public RTSPStreamingSessionFactory(RTSPClientChannel channel, RTSPClientEventManager eventManager )
		{
            _channel = channel;
            _eventManager = eventManager;
        }

        public RTSPStreamingSession NewStreamSession()
        {
            if (_channel.Configuration.DeliveryMode == RTSPDeliveryMode.Tcp )
            {
                throw new NotImplementedException();
            }

            if (_channel.Configuration.DeliveryMode == RTSPDeliveryMode.Udp)
            {
                throw new NotImplementedException();
            }

            if (_channel.Configuration.DeliveryMode == RTSPDeliveryMode.Multicast)
            {
                throw new NotImplementedException();
            }

            throw new NotImplementedException();
        }
    }
}
