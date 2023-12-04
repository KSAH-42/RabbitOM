using System;

namespace RabbitOM.Net.Rtsp.Beta
{
    public class RTSPCommunicationStartedEventArgs : EventArgs{ }
    public class RTSPCommunicationStoppedEventArgs : EventArgs { }
    public class RTSPConnectedEventArgs : EventArgs { }
    public class RTSPDisconnectedEventArgs : EventArgs { }
    public class RTSPStreamingStartedEventArgs : EventArgs { }
    public class RTSPStreamingStatusChangedEventArgs : EventArgs { }
    public class RTSPStreamingStoppedEventArgs : EventArgs { }
    public class RTSPPacketReceivedEventArgs : EventArgs { }
    public class RTSPErrorEventArgs : EventArgs { }
    public class RTSPTransportErrorEventArgs : RTSPErrorEventArgs { }

    public sealed class RTSPClientConfiguration
    {
        public object SyncRoot { get; }
        public string Uri { get;set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public TimeSpan ReceiveTimeout { get; set; }
        public TimeSpan WriteTimeout { get;set; }
        public TimeSpan PingInterval { get; set; }
        public TimeSpan RetriesDelay { get; set; }
        public TimeSpan ReceiveTransportTimeout { get; set; }
        public string MulticastAddress { get; set; }
        public int RtpPort { get; set; }
        public byte TimeToLive { get; set; }
        public bool AutoStartStreaming { get; set; }
        public RTSPMediaFormat MediaFormat { get; set; }
        public RTSPKeepAliveType KeepAliveType { get; set; }
        public RTSPDeliveryMode DeliveryMode { get; set; }
        public RTSPHeaderCollection OptionsHeaders { get;set; }
        public RTSPHeaderCollection DescribeHeaders { get; set; }
        public RTSPHeaderCollection SetupHeaders { get; set; }
        public RTSPHeaderCollection PlayHeaders { get; set; }
        public RTSPHeaderCollection TearDownHeaders { get; set; }
        public RTSPHeaderCollection PingHeaders { get; set; }
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

        public abstract bool IsDispose
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

    public abstract class RTSPClientChannel
    {
        public abstract RTSPClientConfiguration Configuration { get; }
        public abstract void Open();
        public abstract void Close();
        public abstract void Abort();
        public abstract void Setup();
        public abstract void StartStreaming();
        public abstract void StopStreaming();
        public abstract void Ping();
    }

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
    }

    public abstract class RTSPClientConfigurer
    {
        public string Uri { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool PreventIfStarted { get; set; }

        public abstract void Configure();
        public abstract bool TryConfigure();
    }

    public sealed class RTSPClientMulticastConfigurer : RTSPClientConfigurer
    {
        public override void Configure() => throw new NotImplementedException();
        public override bool TryConfigure() => throw new NotImplementedException();
    }

    public abstract class RTSPStreamingSession : IDisposable
    {
        private readonly RTSPClientEventManager _eventManager;  // must not be accessible directly to the child, in order to avoid to raise irrelevant eventargs

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
            _eventManager.PublishEvent(e);
        }

        protected void OnPacketReceived(RTSPPacketReceivedEventArgs e )
        {
            _eventManager.PublishEvent(e);
        }

        protected void OnTransportError(RTSPTransportErrorEventArgs e)
        {
            _eventManager.PublishEvent(e);
        }
    }

    public sealed class RTSPInterleavedStreamingSession : RTSPStreamingSession
    {
        public override bool IsRunning { get; }
        public override bool Setup() => false;
        public override bool StartStreaming() => false;
        public override void StopStreaming() { }
        public override void Dispose() { }
    }

    public sealed class RTSPUdpStreamingSession : RTSPStreamingSession
    {
        public override bool IsRunning { get; }
        public override bool Setup() => throw new NotImplementedException();
        public override bool StartStreaming() => throw new NotImplementedException();
        public override void StopStreaming() { throw new NotImplementedException(); }
        public override void Dispose() { }
    }

    public sealed class RTSPMulticastStreamingSession : RTSPStreamingSession
    {
        public override bool IsRunning { get; }
        public override bool Setup() => throw new NotImplementedException();
        public override bool StartStreaming() => throw new NotImplementedException();
        public override void StopStreaming() { throw new NotImplementedException(); }
        public override void Dispose() { }
    }
}
