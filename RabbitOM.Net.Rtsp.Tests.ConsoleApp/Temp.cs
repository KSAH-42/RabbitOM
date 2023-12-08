using RabbitOM.Net.Rtsp.Remoting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Prototypes
/// </summary>
namespace RabbitOM.Net.Rtsp.Beta
{
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
    public class RTSPStreamingStoppedEventArgs : EventArgs
    {
    }
    public class RTSPStreamingActiveEventArgs : EventArgs
    {
    }
    public class RTSPStreamingInActiveEventArgs : EventArgs
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

    public interface IRTSPEventManager : IDisposable
    {
        void PublishEvent<TEventArgs>(TEventArgs e) where TEventArgs : EventArgs;
    }

    public sealed class RTSPClientEventManager : IRTSPEventManager
    {
        private readonly object _lock;
        private readonly RTSPActionQueue _actions;
        private readonly Dictionary<Type,object> _handlers;
        private readonly RTSPThread _thread;
       
		public RTSPClientEventManager()
		{
            _lock = new object();
            _actions = new RTSPActionQueue();
            _handlers = new Dictionary<Type, object>();
            _thread = new RTSPThread("RTSP - Event Manager");
            _thread.Start( ProcessActions );
        }

        public void PublishEvent<TEventArgs>(TEventArgs e) where TEventArgs : EventArgs
        {
            object handler;

            lock ( _lock )
            {
                _handlers.TryGetValue(typeof(TEventArgs), out handler );
            }

            if ( handler is Action<TEventArgs> eventHandler )
            {
                _actions.Enqueue( () => eventHandler.Invoke( e ) );
            }
        }

        public void RegisterEvent<TEventArgs>( Action<TEventArgs> handler )
        {
            lock ( _lock )
            {
                _handlers[typeof(TEventArgs)] = handler ?? throw new ArgumentNullException();
            }
        }

        public void UnRegisterEvent<TEventArgs>(Action<TEventArgs> handler)
        {
            lock ( _lock )
            {
                var element = _handlers.FirstOrDefault( x => (x.Value as Action<TEventArgs>) != null );

                if ( element.Key != null && element.Value != null )
                {
                    _handlers.Remove( element.Key );
                }
            }
        }

        public void Dispose()
        {
            _thread.Stop();
            _actions.Clear();
            _handlers.Clear();
        }
        
        private void ProcessActions() 
        {
            var pumpAction = new Action( () =>
            {
                while (_actions.TryDequeue(out Action action))
                {
                    action.Invoke();
                }
            });

            while (RTSPActionQueue.Wait(_actions, _thread.ExitHandle))
            {
                pumpAction();
            }

            pumpAction();
        }
    }
}

namespace RabbitOM.Net.Rtsp.Beta
{
    public interface IRTSPClientConfiguration
    {
        object SyncRoot { get; } 
        string Uri { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        TimeSpan ReceiveTimeout { get; set; }
        TimeSpan SendTimeout { get; set; }
        TimeSpan PingInterval { get; set; }
        TimeSpan RetriesDelay { get; set; }
        TimeSpan ReceiveTransportTimeout { get; set; }
        TimeSpan RetriesTransportTimeout { get; set; }
        string MulticastAddress { get; set; }
        int RtpPort { get; set; }
        byte TimeToLive { get; set; }
        RTSPMediaFormat MediaFormat { get; set; }
        RTSPKeepAliveType KeepAliveType { get; set; }
        RTSPDeliveryMode DeliveryMode { get; set; }
        RTSPHeaderCollection OptionsHeaders { get; }
        RTSPHeaderCollection DescribeHeaders { get; }
        RTSPHeaderCollection SetupHeaders { get; }
        RTSPHeaderCollection PlayHeaders { get; }
        RTSPHeaderCollection TearDownHeaders { get; }
        RTSPHeaderCollection PingHeaders { get; } 
    }

    public sealed class RTSPClientConfiguration : IRTSPClientConfiguration
    {
        public object SyncRoot { get; } = new object();
        public string Uri { get;set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public TimeSpan ReceiveTimeout { get; set; }
        public TimeSpan SendTimeout { get;set; }
        public TimeSpan PingInterval { get; set; }
        public TimeSpan RetriesDelay { get; set; }
        public TimeSpan ReceiveTransportTimeout { get; set; }
        public TimeSpan RetriesTransportTimeout { get; set; }
        public string MulticastAddress { get; set; }
        public int RtpPort { get; set; }
        public byte TimeToLive { get; set; }
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
        event EventHandler<RTSPStreamingStoppedEventArgs> StreamingStopped;
        event EventHandler<RTSPStreamingActiveEventArgs> StreamingActive;
        event EventHandler<RTSPStreamingInActiveEventArgs> StreamingInActive;
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

        bool IsReceivingPacket
        {
            get;
        }

        bool StartCommunication();
        void StopCommunication();
        void StopCommunication(TimeSpan shutdownTimeout);
        bool WaitForConnection(TimeSpan timeout);
    }

    public abstract class RTSPClient : IRTSPClient
    {
        public event EventHandler<RTSPCommunicationStartedEventArgs> CommunicationStarted;
        public event EventHandler<RTSPCommunicationStoppedEventArgs> CommunicationStopped;
        public event EventHandler<RTSPConnectedEventArgs> Connected;
        public event EventHandler<RTSPDisconnectedEventArgs> Disconnected;
        public event EventHandler<RTSPStreamingStartedEventArgs> StreamingStarted;
        public event EventHandler<RTSPStreamingStoppedEventArgs> StreamingStopped;
        public event EventHandler<RTSPStreamingActiveEventArgs> StreamingActive;
        public event EventHandler<RTSPStreamingInActiveEventArgs> StreamingInActive;
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
        public abstract bool WaitForConnection(TimeSpan timeout);
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
            StreamingStarted?.TryInvoke(this,e);
        }

        protected virtual void OnStreamingStopped(RTSPStreamingStoppedEventArgs e)
        {
            StreamingStopped?.TryInvoke(this, e);
        }

        protected virtual void OnStreamingActive(RTSPStreamingActiveEventArgs e)
        {
            StreamingActive?.TryInvoke(this, e);
        }

        protected virtual void OnStreamingInActive(RTSPStreamingInActiveEventArgs e)
        {
            StreamingInActive?.TryInvoke(this, e);
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
    public interface IRTSPClientConfigurer
    {
        void Configure();
    }

    public abstract class RTSPClientConfigurer : IRTSPClientConfigurer
    {
        protected readonly IRTSPClient _client;

		protected RTSPClientConfigurer( IRTSPClient client )
		{
            _client = client;
        }

        public string Uri { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
       
        public virtual void Configure()
        {
            _client.Configuration.Uri = Uri;
            _client.Configuration.UserName = UserName;
            _client.Configuration.Password = Password;
            _client.Configuration.ReceiveTimeout = TimeSpan.FromSeconds( 15 );
            _client.Configuration.SendTimeout = TimeSpan.FromSeconds(15);
            _client.Configuration.RetriesDelay = TimeSpan.FromSeconds(15);
            _client.Configuration.PingInterval = TimeSpan.FromSeconds(15);
            _client.Configuration.RetriesTransportTimeout = TimeSpan.FromSeconds(15);
        }
    }

    public sealed class RTSPClientTcpConfigurer : RTSPClientConfigurer
    {
        public RTSPClientTcpConfigurer(IRTSPClient client)
            : base( client )
        {
        }

        public override void Configure()
        {
            _client.Configuration.DeliveryMode = RTSPDeliveryMode.Tcp;

            base.Configure();
        }
    }

    public sealed class RTSPClientUdpConfigurer : RTSPClientConfigurer
    {
        public RTSPClientUdpConfigurer(IRTSPClient client)
            : base(client)
        {
        }

        public int RtpPort {get; set; } = 34001;
        public TimeSpan ReceiveTransportTimeout { get; set; } = TimeSpan.FromSeconds(15);
        public TimeSpan RetriesTransportTimeout { get; set; } = TimeSpan.FromSeconds(15);
        
        public override void Configure()
        {
            _client.Configuration.DeliveryMode = RTSPDeliveryMode.Udp;
            _client.Configuration.ReceiveTransportTimeout = ReceiveTransportTimeout;
            _client.Configuration.RetriesTransportTimeout = RetriesTransportTimeout;
            _client.Configuration.RtpPort = RtpPort;

            base.Configure();
        }
    }

    public sealed class RTSPClientMulticastConfigurer : RTSPClientConfigurer
    {
        public RTSPClientMulticastConfigurer(IRTSPClient client)
            : base(client)
        {
        }

        public string IPAddress {get;set; } = "224.0.0.1";
        public int RtpPort { get; set; } = 35001;
        public byte TimeToLive { get; set; } = 16;
        public TimeSpan ReceiveTransportTimeout { get; set; } = TimeSpan.FromSeconds(15);
        public TimeSpan RetriesTransportTimeout { get; set; } = TimeSpan.FromSeconds(15);

        public override void Configure()
        {
            _client.Configuration.DeliveryMode = RTSPDeliveryMode.Multicast;
            _client.Configuration.MulticastAddress = IPAddress;
            _client.Configuration.RtpPort = RtpPort;
            _client.Configuration.TimeToLive = TimeToLive;
            _client.Configuration.ReceiveTransportTimeout = ReceiveTransportTimeout;
            _client.Configuration.RetriesTransportTimeout = RetriesTransportTimeout;

            base.Configure();
        }
    }
}

namespace RabbitOM.Net.Rtsp.Beta
{
    public interface IRTSPStreamingChannel : IDisposable
    {
        object SyncRoot {get; }
        IRTSPClientConfiguration Configuration { get; }
        bool IsOpened { get; }
        bool IsStreamingStarted { get; }
        bool IsReceivingPacket { get; }
        bool HasErrors { get; }

        bool Open();
        void Close();
        void Abort();
        bool StartStreaming();
        void StopStreaming();
        bool Ping();
        void WaitForConnection( TimeSpan timeout );
    }

    public abstract class RTSPStreamingChannel : IRTSPStreamingChannel
    {
        private readonly IRTSPEventManager _eventManager;
        
		protected RTSPStreamingChannel( IRTSPEventManager eventManager )
		{                     
            _eventManager = eventManager;
        }

        public abstract object SyncRoot { get; }
        public abstract IRTSPClientConfiguration Configuration { get; }
        public abstract bool IsOpened { get; }
        public abstract bool IsStreamingStarted { get; }
        public abstract bool IsReceivingPacket { get; }
        public abstract bool HasErrors { get; }

        public abstract bool Open();
        public abstract void Close();
        public abstract void Dispose();
        public abstract void Abort();
        public abstract bool StartStreaming();
        public abstract void StopStreaming();
        public abstract bool Ping();
        public abstract void WaitForConnection(TimeSpan timeout);

        protected virtual void OnOpen()
        {
            _eventManager.PublishEvent( new RTSPConnectedEventArgs() );
        }

        protected virtual void OnClose()
        {
            _eventManager.PublishEvent(new RTSPDisconnectedEventArgs());
        }
    }

    public sealed class RTSPChannelRunner : IDisposable
    {
        private readonly IRTSPStreamingChannel _channel;
        
		public RTSPChannelRunner(IRTSPStreamingChannel channel)
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

                if (! _channel.StartStreaming() )
                {
                    _channel.Close();

                    return;
                }

                IdleTimeout = _channel.Configuration.PingInterval;
            }
            else
            {
                if ( _channel.Ping() )
                {
                    return;
                }

                if ( _channel.IsStreamingStarted )
                {
                    _channel.StopStreaming();
                }

                _channel.Close();
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
        private readonly IRTSPEventManager _eventManager;  // must not be accessible directly to the child, in order to avoid to raise irrelevant eventargs

        protected RTSPStreamingSession(IRTSPEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        public abstract string SessionId {get; }
        public abstract bool IsRunning { get; }
        public abstract bool Describe();
        public abstract bool Setup();
        public abstract bool StartStreaming();
        public abstract void StopStreaming();
        public abstract void Dispose();

        protected void OnStreamingActive(RTSPStreamingActiveEventArgs e) 
        {
            _eventManager.PublishEvent( e );
        }

        protected void OnStreamingStarted(RTSPStreamingStartedEventArgs e)
        {
            _eventManager.PublishEvent(e);
        }

        protected void OnStreamingStopped(RTSPStreamingStoppedEventArgs e)
        {
            _eventManager.PublishEvent(e);
        }

        protected void OnStreamingInActive(RTSPStreamingInActiveEventArgs e)
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

    public sealed class RTSPTcpStreamingSession : RTSPStreamingSession
    {
        private readonly IRTSPStreamingChannel _channel;
        private readonly IRTSPConnection _connection;

        public RTSPTcpStreamingSession(IRTSPStreamingChannel channel, IRTSPConnection connection, IRTSPEventManager eventManager)
            : base( eventManager )
		{
            _channel = channel;
            _connection = connection;
        }

        public override string SessionId { get; }
        public override bool IsRunning { get; }
        public override bool Describe() => false;
        public override bool Setup() => false;
        public override bool StartStreaming() => false;
        public override void StopStreaming() { }
        public override void Dispose() { }
    }

    public sealed class RTSPUdpStreamingSession : RTSPStreamingSession
    {
        private readonly IRTSPStreamingChannel _channel;
        private readonly IRTSPConnection _connection;

        public RTSPUdpStreamingSession(IRTSPStreamingChannel channel, IRTSPConnection connection, IRTSPEventManager eventManager)
            : base(eventManager)
		{
            _channel = channel;
            _connection = connection;
		}

        public override string SessionId { get; }
        public override bool IsRunning { get; }
        public override bool Describe() => false;
        public override bool Setup() => throw new NotImplementedException();
        public override bool StartStreaming() => throw new NotImplementedException();
        public override void StopStreaming() { throw new NotImplementedException(); }
        public override void Dispose() { }
    }

    public sealed class RTSPMulticastStreamingSession : RTSPStreamingSession
    {
        private readonly IRTSPStreamingChannel _channel;
        private readonly IRTSPConnection _connection;

        public RTSPMulticastStreamingSession(IRTSPStreamingChannel channel, IRTSPConnection connection, IRTSPEventManager eventManager)
            : base(eventManager)
		{
            _channel = channel;
            _connection = connection;
		}

        public override string SessionId { get; }
        public override bool IsRunning { get; }
        public override bool Describe() => false;
        public override bool Setup() => throw new NotImplementedException();
        public override bool StartStreaming() => throw new NotImplementedException();
        public override void StopStreaming() { throw new NotImplementedException(); }
        public override void Dispose() { }
    }

    public sealed class RTSPStreamingSessionFactory
    {
        private readonly IRTSPStreamingChannel _channel;
        private readonly IRTSPConnection _connection;
        private readonly IRTSPEventManager _eventManager;

        public RTSPStreamingSessionFactory(IRTSPStreamingChannel channel, IRTSPConnection connection, IRTSPEventManager eventManager )
		{
            _channel = channel;
            _connection = connection;
            _eventManager = eventManager;
        }

        public RTSPStreamingSession NewStreamSession()
        {
            if (_channel.Configuration.DeliveryMode == RTSPDeliveryMode.Tcp )
            {
                return new RTSPTcpStreamingSession( _channel , _connection , _eventManager );
            }

            if (_channel.Configuration.DeliveryMode == RTSPDeliveryMode.Udp)
            {
                return new RTSPUdpStreamingSession( _channel , _connection , _eventManager );
            }

            if (_channel.Configuration.DeliveryMode == RTSPDeliveryMode.Multicast)
            {
                return new RTSPMulticastStreamingSession( _channel , _connection , _eventManager );
            }

            throw new NotSupportedException( $"The streaming session using {_channel.Configuration.DeliveryMode} is not supported");
        }
    }
}
