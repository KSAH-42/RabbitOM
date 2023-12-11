using RabbitOM.Net.Rtsp.Remoting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;

/// <summary>
/// Prototypes
/// </summary>
/// 
/// Add MulticastClient , UdpClient, etc... ?
/// 
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
        private readonly RTSPActionQueue _actions;
        private readonly System.Collections.Concurrent.ConcurrentDictionary<Type,object> _handlers;
        private readonly RTSPThread _thread;
       
		public RTSPClientEventManager()
		{
            _actions = new RTSPActionQueue();
            _handlers = new System.Collections.Concurrent.ConcurrentDictionary<Type, object>();
            _thread = new RTSPThread("RTSP - Event Manager");
            _thread.Start( ProcessActions );
        }

        public void PublishEvent<TEventArgs>(TEventArgs e) where TEventArgs : EventArgs
        {
            object handler;

            _handlers.TryGetValue(typeof(TEventArgs), out handler);

            if ( handler is Action<TEventArgs> eventHandler )
            {
                _actions.Enqueue( () => eventHandler.Invoke( e ) );
            }
        }

        public void RegisterEvent<TEventArgs>( Action<TEventArgs> handler )
        {
            _handlers[typeof(TEventArgs)] = handler ?? throw new ArgumentNullException();
        }

        public void UnRegisterEvent<TEventArgs>(Action<TEventArgs> handler)
        {
            var element = _handlers.FirstOrDefault(x => (x.Value as Action<TEventArgs>) != null);

            if (element.Key != null && element.Value != null)
            {
                _handlers.TryRemove(element.Key, out object result);
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

        IRTSPClientConfiguration Configuration
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

        public abstract IRTSPClientConfiguration Configuration
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
            base.Configure();

            _client.Configuration.DeliveryMode = RTSPDeliveryMode.Tcp;
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
            base.Configure();

            _client.Configuration.DeliveryMode = RTSPDeliveryMode.Udp;
            _client.Configuration.ReceiveTransportTimeout = ReceiveTransportTimeout;
            _client.Configuration.RetriesTransportTimeout = RetriesTransportTimeout;
            _client.Configuration.RtpPort = RtpPort;
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
            base.Configure();

            _client.Configuration.DeliveryMode = RTSPDeliveryMode.Multicast;
            _client.Configuration.MulticastAddress = IPAddress;
            _client.Configuration.RtpPort = RtpPort;
            _client.Configuration.TimeToLive = TimeToLive;
            _client.Configuration.ReceiveTransportTimeout = ReceiveTransportTimeout;
            _client.Configuration.RetriesTransportTimeout = RetriesTransportTimeout;
        }
    }
}

namespace RabbitOM.Net.Rtsp.Beta
{
    public interface IRTSPChannel : IDisposable
    {
        object SyncRoot {get; }
        IRTSPClientConfiguration Configuration { get; }
        bool IsOpened { get; }
        bool IsStreamingStarted { get; }
        bool IsReceivingPacket { get; }

        bool Open();
        void Close();
        void Abort();
        bool StartStreaming();
        void StopStreaming();
        bool Ping();
        void WaitForConnection( TimeSpan timeout );
    }

    public abstract class RTSPChannel : IRTSPChannel
    {
        private readonly IRTSPEventManager _eventManager;
        
		protected RTSPChannel( IRTSPEventManager eventManager )
		{                     
            _eventManager = eventManager;
        }

        public abstract object SyncRoot { get; }
        public abstract IRTSPClientConfiguration Configuration { get; }
        public abstract bool IsOpened { get; }
        public abstract bool IsStreamingStarted { get; }
        public abstract bool IsReceivingPacket { get; }

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
        private readonly IRTSPChannel _channel;
        
		public RTSPChannelRunner(IRTSPChannel channel)
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
        public abstract bool IsDescribed { get; }
        public abstract bool IsSetup { get; }
        public abstract bool IsRunning { get; }
        public abstract bool Describe();
        public abstract bool Setup();
        public abstract bool StartStreaming();
        public abstract void StopStreaming();
        public abstract bool Ping();
        public abstract void Dispose();

        protected void OnStreamingActive(RTSPStreamingActiveEventArgs e) 
        {
            _eventManager.PublishEvent( e );
        }

        protected void OnStreamingInActive(RTSPStreamingInActiveEventArgs e)
        {
            _eventManager.PublishEvent(e);
        }
        
        protected void OnStreamingStarted(RTSPStreamingStartedEventArgs e)
        {
            _eventManager.PublishEvent(e);
        }

        protected void OnStreamingStopped(RTSPStreamingStoppedEventArgs e)
        {
            _eventManager.PublishEvent(e);
        }

        protected void OnPacketReceived(RTSPPacketReceivedEventArgs e )
        {
            _eventManager.PublishEvent(e);
        }

        protected void OnError(RTSPErrorEventArgs e)
        {
            _eventManager.PublishEvent(e);
        }
    }

    public sealed class RTSPTcpStreamingSession : RTSPStreamingSession
    {
        private readonly IRTSPChannel _channel;
        private readonly IRTSPConnection _connection;

        public RTSPTcpStreamingSession(IRTSPChannel channel, IRTSPConnection connection, IRTSPEventManager eventManager)
            : base( eventManager )
		{
            _channel = channel;
            _connection = connection;
        }

        public override string SessionId { get; }
        public override bool IsDescribed { get; }
        public override bool IsSetup { get; }
        public override bool IsRunning { get; }
        public override bool Describe() => false;
        public override bool Setup() => false;
        public override bool StartStreaming() => false;
        public override void StopStreaming() { }
        public override bool Ping() => false;
        public override void Dispose() { }
    }

    public sealed class RTSPUdpStreamingSession : RTSPStreamingSession
    {
        private readonly IRTSPChannel _channel;
        private readonly IRTSPConnection _connection;

        public RTSPUdpStreamingSession(IRTSPChannel channel, IRTSPConnection connection, IRTSPEventManager eventManager)
            : base(eventManager)
		{
            _channel = channel;
            _connection = connection;
		}

        public override string SessionId { get; }
        public override bool IsDescribed { get; }
        public override bool IsSetup { get; }
        public override bool IsRunning { get; }
        public override bool Describe() => false;
        public override bool Setup() => throw new NotImplementedException();
        public override bool StartStreaming() => throw new NotImplementedException();
        public override void StopStreaming() { throw new NotImplementedException(); }
        public override bool Ping() => false; 
        public override void Dispose() { }
    }

    public sealed class RTSPMulticastStreamingSession : RTSPStreamingSession
    {
        private readonly IRTSPChannel _channel;
        private readonly IRTSPConnection _connection;

        public RTSPMulticastStreamingSession(IRTSPChannel channel, IRTSPConnection connection, IRTSPEventManager eventManager)
            : base(eventManager)
		{
            _channel = channel;
            _connection = connection;
		}

        public override string SessionId { get; }
        public override bool IsDescribed { get; }
        public override bool IsSetup { get; }
        public override bool IsRunning { get; }
        public override bool Describe() => false;
        public override bool Setup() => throw new NotImplementedException();
        public override bool StartStreaming() => throw new NotImplementedException();
        public override void StopStreaming() { throw new NotImplementedException(); }
        public override bool Ping() => false; 
        public override void Dispose() { }
    }

    public sealed class RTSPStreamingSessionFactory
    {
        private readonly IRTSPChannel _channel;
        private readonly IRTSPConnection _connection;
        private readonly IRTSPEventManager _eventManager;

        public RTSPStreamingSessionFactory(IRTSPChannel channel, IRTSPConnection connection, IRTSPEventManager eventManager )
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



//////////////////////////////////////////////////




namespace RabbitOM.Net.Rtsp.Streaming
{
    public interface IRTSPConfiguration
    {
        object SyncRoot { get; }
        string Uri { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        TimeSpan ReceiveTimeout { get; set; }
        TimeSpan SendTimeout { get; set; }
        RTSPKeepAliveType KeepAliveType { get; set; }
    }

    public interface IRTSPClient
    {
        event EventHandler CommunicationStarted;
        event EventHandler CommunicationStopped;
        event EventHandler Connected;
        event EventHandler Disconnected;
        event EventHandler StreamingStarted;
        event EventHandler StreamingStopped;
        event EventHandler PacketReceived;
        event EventHandler Error;

        object SyncRoot { get; }
        IRTSPConfiguration Configuration { get; }
        bool IsCommunicationStarted { get; }
        bool IsConnected { get; }
        bool IsReceivingPacket { get; }
        bool IsStreamingStarted { get; }

        bool StartCommunication();
        void StopCommunication();
        void StopCommunication(TimeSpan shutdownTimeout);
        bool WaitForConnection(TimeSpan shutdownTimeout);
    }

    public abstract class RTSPClient : IRTSPClient
    {
        public event EventHandler CommunicationStarted;
        public event EventHandler CommunicationStopped;
        public event EventHandler Connected;
        public event EventHandler Disconnected;
        public event EventHandler StreamingStarted;
        public event EventHandler StreamingStopped;
        public event EventHandler PacketReceived;
        public event EventHandler Error;

        public abstract object SyncRoot { get; }
        public abstract IRTSPConfiguration Configuration { get; }
        public abstract bool IsCommunicationStarted { get; }
        public abstract bool IsConnected { get; }
        public abstract bool IsReceivingPacket { get; }
        public abstract bool IsStreamingStarted { get; }

        public abstract bool StartCommunication();
        public abstract void StopCommunication();
        public abstract void StopCommunication(TimeSpan shutdownTimeout);
        public abstract bool WaitForConnection(TimeSpan shutdownTimeout);

        protected void RaiseEvent(EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnCommunicationStarted(EventArgs e)
            => CommunicationStarted.TryInvoke(this, e);
        protected virtual void OnCommunicationStopped(EventArgs e)
            => CommunicationStopped.TryInvoke(this, e);
        protected virtual void OnConnected(EventArgs e)
            => Connected.TryInvoke(this, e);
        protected virtual void OnDisconnected(EventArgs e)
            => Disconnected.TryInvoke(this, e);
        protected virtual void OnStreamingStarted(EventArgs e)
            => StreamingStarted.TryInvoke(this, e);
        protected virtual void OnStreamingStopped(EventArgs e)
            => StreamingStopped.TryInvoke(this, e);
        protected virtual void OnPacketReceived(EventArgs e)
            => PacketReceived.TryInvoke(this, e);
        protected virtual void OnError(EventArgs e)
            => Error.TryInvoke(this, e);
    }

    public sealed class RTSPEventManager : IDisposable
    {
        private readonly Action<EventArgs> _handler;
        private readonly RTSPEventQueue _queue;
        private readonly RTSPThread _thread;

		public RTSPEventManager( Action<EventArgs> handler )
		{
            _handler = handler;
            _queue = new RTSPEventQueue();
            _thread = new RTSPThread( "EventLoop" );
        }

        public void DispatchEvent( EventArgs e )
        {
            _queue.Enqueue( e );
        }

        public void RaiseEvent(EventArgs e)
        {
            _handler.Invoke( e );
        }

        public void Start()
        {
            _thread.Start(DoEvents);
        }

        public void Stop()
        {
            _thread.Stop();
        }

        public void Dispose()
        {
            Stop();
        }

        private void DoEvents()
        {
            var pumpEvents = new Action( () =>
            {
                while ( _queue.TryDequeue( out EventArgs e ) )
                {
                    _handler.Invoke( e );
                }
            });

            while ( RTSPEventQueue.Wait( _queue , _thread.ExitHandle ) )
            {
                pumpEvents();
            }

            pumpEvents();
        }
    }

    public class RTSPMediaClient : RTSPClient
    {
        private readonly RTSPThread _thread;
        private readonly RTSPMediaChannel _channel;

        public override object SyncRoot { get => _channel.SyncRoot; }
        public override IRTSPConfiguration Configuration { get => _channel.Configuration; }
        public override bool IsCommunicationStarted { get => _thread.IsStarted; }
        public override bool IsConnected { get => _channel.IsConnected; }
        public override bool IsReceivingPacket { get => _channel.IsReceivingPacket; }
        public override bool IsStreamingStarted { get => _channel.IsStreamingStarted; }

        public override bool StartCommunication()
        {
            return _thread.Start(() =>
            {
                OnCommunicationStarted(EventArgs.Empty);

                using (var runner = new RTSPMediaChannelRunner(_channel))
                {
                    while (_thread.CanContinue(runner.IdleTimeout))
                    {
                        runner.Run();
                    }
                }

                OnCommunicationStopped(EventArgs.Empty);
            });
        }

        public override void StopCommunication()
        {
            _thread.Stop();
        }

        public override void StopCommunication(TimeSpan shutdownTimeout)
        {
            if (!_thread.Join(shutdownTimeout))
            {
                _channel.Abort();
            }

            _thread.Stop();
        }

        public override bool WaitForConnection(TimeSpan shutdownTimeout)
        {
            return _channel.WaitForConnection(shutdownTimeout);
        }
    }

    // RTSPClient
    // RTSPMediaClient
    // RTSPMediaChannel
    // RTSPMediaChannelRunner
    // RTSPMediaTransport
    // RTSPMediaTransportTcp
    // RTSPMediaTransportUdp
    // RTSPMediaTransportMutlticast

    public sealed class RTSPMediaChannelRunner : IDisposable
    {
        private readonly RTSPMediaChannel _channel;

        public RTSPMediaChannelRunner(RTSPMediaChannel channel)
        {
            _channel = channel;
            _channel.EventManager.Start();
        }

        public TimeSpan IdleTimeout { get; private set; }

        public void Run()
        {
            if (!_channel.IsConnected)
            {
                if (!_channel.Connect())
                {
                    return;
                }

                if (!_channel.StartStreaming())
                {
                    _channel.Close();
                }
            }
            else
            {
                if (!_channel.Ping())
                {
                    _channel.Close();
                }
            }
        }

        public void Dispose()
        {
            if (_channel.IsConnected)
            {
                if (_channel.IsStreamingStarted)
                {
                    _channel.StopStreaming();
                }

                _channel.Close();
            }

            _channel.EventManager.Stop();
        }

        public void AddHook<TEventArgs>(Action<TEventArgs> action) { }
        public void RemoveEventHandler<TEventArgs>(Action<TEventArgs> action) { }
    }

    public sealed class RTSPMediaChannel : IDisposable
    {
        public object SyncRoot
            => throw new NotImplementedException();
        public IRTSPConfiguration Configuration
            => throw new NotImplementedException();
        public RTSPEventManager EventManager
            => throw new NotImplementedException();
        public bool IsConnected
            => throw new NotImplementedException();
        public bool IsReceivingPacket
            => throw new NotImplementedException();
        public bool IsStreamingStarted 
            => throw new NotImplementedException();

        public bool Connect()
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
        public bool Ping()
            => throw new NotImplementedException();
        public bool WaitForConnection(TimeSpan shutdownTimeout)
            => throw new NotImplementedException();
    }
}





