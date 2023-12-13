using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitOM.Net.Rtsp.Beta
{
    using RabbitOM.Net.Rtsp.Remoting;

    public enum RTSPStreamingStatus
    {
        InActive = 0 , Active
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
    public class RTSPStreamingStoppedEventArgs : EventArgs
    {
    }
    public class RTSPStreamingStatusChangedEventArgs : EventArgs
    {
        public RTSPStreamingStatus Status { get; private set; } 
    }
    public class RTSPErrorEventArgs : EventArgs
    {
    }
    public class RTSPTransportErrorEventArgs : RTSPErrorEventArgs
    {
    }
    public class RTSPMessageReceivedEventArgs : EventArgs
    {
        public bool Canceled { get; set; }
        public RTSPMessageRequest Request { get; private set; }
        public RTSPMessageResponse Response { get; private set; }
    }

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
        RTSPHeaderCollection OptionsHeaders {get; }
        RTSPHeaderCollection DescribeHeaders { get; }
        RTSPHeaderCollection SetupHeaders { get; }
        RTSPHeaderCollection PlayHeaders { get; }
        RTSPHeaderCollection TearDownHeaders { get; }
        RTSPHeaderCollection PingHeaders { get; }
    }

    public sealed class RTSPClientConfiguration : IRTSPClientConfiguration
    {
        public object SyncRoot { get; } = new object();
        public string Uri { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public TimeSpan ReceiveTimeout { get; set; }
        public TimeSpan SendTimeout { get; set; }
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
        public RTSPHeaderCollection OptionsHeaders { get; }
        public RTSPHeaderCollection DescribeHeaders { get; }
        public RTSPHeaderCollection SetupHeaders { get; }
        public RTSPHeaderCollection PlayHeaders { get; }
        public RTSPHeaderCollection TearDownHeaders { get; }
        public RTSPHeaderCollection PingHeaders { get; }
    }

    public interface IRTSPClient : IDisposable
    {
        event EventHandler<RTSPCommunicationStartedEventArgs> CommunicationStarted;
        event EventHandler<RTSPCommunicationStoppedEventArgs> CommunicationStopped;
        event EventHandler<RTSPConnectedEventArgs> Connected;
        event EventHandler<RTSPDisconnectedEventArgs> Disconnected;
        event EventHandler<RTSPStreamingStartedEventArgs> StreamingStarted;
        event EventHandler<RTSPStreamingStoppedEventArgs> StreamingStopped;
        event EventHandler<RTSPStreamingStatusChangedEventArgs> StreamingStatusChanged;
        event EventHandler<RTSPMessageReceivedEventArgs> MessageReceived;
        event EventHandler<RTSPPacketReceivedEventArgs> PacketReceived;
        event EventHandler<RTSPErrorEventArgs> Error;

        object SyncRoot { get; }
        IRTSPClientConfiguration Configuration { get; }
        bool IsDisposed { get; }
        bool IsConnected { get; }
        bool IsReceivingPacket { get; }
        bool IsStreamingStarted { get; }
        bool IsCommunicationStarted { get; }
        

        bool StartCommunication();
        void StopCommunication();
        void StopCommunication(TimeSpan shutdownTimeout);
        bool WaitForConnection(TimeSpan shutdownTimeout);
        Task<bool> WaitForConnectionAsync(TimeSpan shutdownTimeout);
    }

    public interface IRTSPClientDispatcher
    {
        void Dispatch( EventArgs e );
        void RaiseEvent(EventArgs e);
    }

    public sealed class RTSPEventManagerManager
    {
        private readonly IRTSPClientDispatcher _dispatcher;

        public RTSPEventManagerManager(IRTSPClientDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void NotifyCommunicationStarted( RTSPCommunicationStartedEventArgs e )
        {
            _dispatcher.Dispatch( e );
        }

        public void NotifyCommunicationStopped(RTSPCommunicationStoppedEventArgs e)
        {
            _dispatcher.Dispatch(e);
        }

        public void NotifyMessageReceived(RTSPMessageReceivedEventArgs e)
        {
            _dispatcher.RaiseEvent(e);
        }

        // etc...
    }

    public sealed class RTSPClientDispatcher : IRTSPClientDispatcher , IDisposable
    {
        private readonly Action<EventArgs> _handler;
        private readonly RTSPEventQueue _queue;
        private readonly RTSPThread _thread;

        public RTSPClientDispatcher( Action<EventArgs> handler )
        {
            _handler = handler;
            _queue = new RTSPEventQueue();
            _thread = new RTSPThread( "RTSP - Dispatcher thread" );
        }

        public void Start()
        {
            _thread.Start( DoEvents );
        }

        public void Stop()
        {
            _thread.Stop();
            _queue.Clear();
        }

        public void Dispose()
        {
            Stop();
        }

        public void Dispatch( EventArgs e )
        {
            _queue.Enqueue( e );
        }

        public void RaiseEvent( EventArgs e )
        {
            _handler.Invoke( e );
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

    public class RTSPClient : IRTSPClient
    {
        public event EventHandler<RTSPCommunicationStartedEventArgs> CommunicationStarted;
        public event EventHandler<RTSPCommunicationStoppedEventArgs> CommunicationStopped;
        public event EventHandler<RTSPConnectedEventArgs> Connected;
        public event EventHandler<RTSPDisconnectedEventArgs> Disconnected;
        public event EventHandler<RTSPStreamingStartedEventArgs> StreamingStarted;
        public event EventHandler<RTSPStreamingStoppedEventArgs> StreamingStopped;
        public event EventHandler<RTSPStreamingStatusChangedEventArgs> StreamingStatusChanged;
        public event EventHandler<RTSPMessageReceivedEventArgs> MessageReceived; 
        public event EventHandler<RTSPPacketReceivedEventArgs> PacketReceived;
        public event EventHandler<RTSPErrorEventArgs> Error;


        private readonly RTSPThread _thread;
        private readonly RTSPMediaChannel _channel; 
        private readonly RTSPClientDispatcher _dispatcher;

        public RTSPClient()
        {
            _dispatcher = new RTSPClientDispatcher( RaiseEvent );
            _channel = new RTSPMediaChannel(_dispatcher);
            _thread = new RTSPThread( "RTSP - Client thread" );
        }

        public object SyncRoot 
            => _channel.SyncRoot;

        public IRTSPClientConfiguration Configuration 
            => _channel.Configuration;

        public bool IsDisposed 
            => _channel.IsDisposed;

        public bool IsConnected
            => _channel.IsConnected;

        public bool IsReceivingPacket
            => _channel.IsReceivingPacket;

        public bool IsStreamingStarted
            => _channel.IsStreamingStarted;

        public bool IsCommunicationStarted
            => _thread.IsStarted;

        protected RTSPClientDispatcher Dispatcher 
            => _dispatcher;



        public bool StartCommunication()
        {
            _dispatcher.Start();

            return _thread.Start( () => 
            {
                OnCommunicationStarted( new RTSPCommunicationStartedEventArgs() );
                
                using ( var runner = new RTSPMediaChannelRunner( _channel ) )
                {
                    while ( _thread.CanContinue( runner.IdleTimeout ) )
                    {
                        runner.Run();
                    }
                }

                OnCommunicationStopped(new RTSPCommunicationStoppedEventArgs() );
            });
        }

        public void StopCommunication()
        {
            _thread.Stop();
            _dispatcher.Stop();
        }

        public void StopCommunication(TimeSpan shutdownTimeout)
        {
            if ( _thread.Join( shutdownTimeout ) )
            {
                _channel.Abort();
            }

            _thread.Stop();
            _dispatcher.Stop();
        }

        public void Dispose()
        {
            StopCommunication();
            _channel.Dispose();
        }

        public bool WaitForConnection(TimeSpan shutdownTimeout)
        {
            return _channel.WaitForConnection( shutdownTimeout );
        }

        public async Task<bool> WaitForConnectionAsync(TimeSpan shutdownTimeout)
        {
            return await Task.Run( () => _channel.WaitForConnection( shutdownTimeout ) );
        }

        protected void RaiseEvent(EventArgs e)
        {
            if ( e is RTSPPacketReceivedEventArgs )
            {
                OnPacketReceived( e as RTSPPacketReceivedEventArgs );
            }
            else if (e is RTSPMessageReceivedEventArgs)
            {
                OnMessageReceived(e as RTSPMessageReceivedEventArgs);
            }
            else if ( e is RTSPCommunicationStartedEventArgs )
            {
                OnCommunicationStarted( e as RTSPCommunicationStartedEventArgs );
            }
            else if (e is RTSPCommunicationStoppedEventArgs)
            {
                OnCommunicationStopped(e as RTSPCommunicationStoppedEventArgs);
            }
            else if (e is RTSPConnectedEventArgs)
            {
                OnConnected(e as RTSPConnectedEventArgs);
            }
            else if (e is RTSPDisconnectedEventArgs)
            {
                OnDisconnected(e as RTSPDisconnectedEventArgs);
            }
            else if (e is RTSPStreamingStartedEventArgs)
            {
                OnStreamingStarted(e as RTSPStreamingStartedEventArgs);
            }
            else if (e is RTSPStreamingStoppedEventArgs)
            {
                OnStreamingStopped(e as RTSPStreamingStoppedEventArgs);
            }
            else if (e is RTSPStreamingStatusChangedEventArgs)
            {
                OnStreamingStatusChanged(e as RTSPStreamingStatusChangedEventArgs);
            }
            else if (e is RTSPErrorEventArgs)
            {
                OnError(e as RTSPErrorEventArgs);
            }
        }

        protected virtual void OnCommunicationStarted(RTSPCommunicationStartedEventArgs e)
            => CommunicationStarted.TryInvoke(this, e);
        protected virtual void OnCommunicationStopped(RTSPCommunicationStoppedEventArgs e)
            => CommunicationStopped.TryInvoke(this, e);
        protected virtual void OnConnected(RTSPConnectedEventArgs e)
            => Connected.TryInvoke(this, e);
        protected virtual void OnDisconnected(RTSPDisconnectedEventArgs e)
            => Disconnected.TryInvoke(this, e);
        protected virtual void OnStreamingStarted(RTSPStreamingStartedEventArgs e)
            => StreamingStarted.TryInvoke(this, e);
        protected virtual void OnStreamingStopped(RTSPStreamingStoppedEventArgs e)
            => StreamingStopped.TryInvoke(this, e);
        protected virtual void OnStreamingStatusChanged(RTSPStreamingStatusChangedEventArgs e)
            => StreamingStatusChanged.TryInvoke(this, e);
        protected virtual void OnMessageReceived(RTSPMessageReceivedEventArgs e)
            => MessageReceived.TryInvoke(this, e);
        protected virtual void OnPacketReceived(RTSPPacketReceivedEventArgs e)
            => PacketReceived.TryInvoke(this, e);
        protected virtual void OnError(RTSPErrorEventArgs e)
            => Error.TryInvoke(this, e);
    }

    public sealed class RTSPMediaChannelRunner : IDisposable
    {
        private readonly IRTSPMediaChannel _channel;

        public RTSPMediaChannelRunner(IRTSPMediaChannel channel)
        {
            _channel = channel;
        }

        public TimeSpan IdleTimeout { get; private set; }

        public void Run()
        {
            if (!_channel.IsConnected)
            {
                IdleTimeout = _channel.Configuration.RetriesDelay;

                if (!_channel.Connect())
                {
                    return;
                }

                if (!_channel.StartStreaming())
                {
                    _channel.Close();
                }

                IdleTimeout = _channel.Configuration.PingInterval;
            }
            else
            {
                if (!_channel.Ping())
                {
                    _channel.Close();

                    IdleTimeout = _channel.Configuration.RetriesDelay;
                }
            }
        }

        public void Dispose()
        {
            if ( _channel.IsConnected )
            {
                if (_channel.IsStreamingStarted)
                {
                    _channel.StopStreaming();
                }

                _channel.Close();
            }
        }
    }

    public interface IRTSPMediaChannel : IDisposable
    {
        object SyncRoot { get; }
        IRTSPClientConfiguration Configuration { get; }
        IRTSPClientDispatcher Dispatcher { get; }
        bool IsConnected { get; }
        bool IsReceivingPacket { get; }
        bool IsStreamingStarted { get; }
        bool Connect();
        bool Close();
        void Abort();
        bool StartStreaming();
        bool StopStreaming();
        bool Ping();
        bool WaitForConnection(TimeSpan shutdownTimeout);
    }

    // Need to used and pass 
    // the RTSPEventManagerManager ?

    public sealed class RTSPMediaChannel : IRTSPMediaChannel
    {
        private readonly IRTSPClientDispatcher _dispatcher;

        public RTSPMediaChannel(IRTSPClientDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public object SyncRoot
            => throw new NotImplementedException();
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



        private void OnConnected(RTSPConnectedEventArgs e)
            => _dispatcher.Dispatch( e );
        private void OnDisconnected(RTSPDisconnectedEventArgs e)
            => _dispatcher.Dispatch(e);
        private void OnMessageReceived(RTSPMessageReceivedEventArgs e)
            => _dispatcher.RaiseEvent(e);
        private void OnError(RTSPErrorEventArgs e)
            => _dispatcher.Dispatch(e);
    }

    public abstract class RTSPMediaTransport : IDisposable
    {
        private readonly IRTSPMediaChannel _channel;
        private readonly IRTSPConnection _connection;

        protected RTSPMediaTransport(IRTSPMediaChannel channel, IRTSPConnection connection)
        {
            _channel = channel;
            _connection = connection;
        }

        protected IRTSPMediaChannel Channel { get => _channel; }
        protected IRTSPConnection Connection { get => _connection; }

        public abstract object SyncRoot { get; }
        public abstract bool HasOptions { get; }
        public abstract bool IsDescribed { get; }
        public abstract bool IsSetup { get; }
        public abstract bool IsStarted { get; }

        public abstract bool Options();
        public abstract bool Describe();
        public abstract bool Setup();
        public abstract bool Play();
        public abstract void TearDown();
        public abstract void Dispose();

        protected virtual void OnStreamingStarted(RTSPStreamingStartedEventArgs e )
        {
            _channel.Dispatcher.Dispatch( e );
        }

        protected virtual void OnStreamingStopped(RTSPStreamingStoppedEventArgs e)
        {
            _channel.Dispatcher.Dispatch( e );
        }

        protected virtual void OnStreamingStatusChanged(RTSPStreamingStatusChangedEventArgs e)
        {
            _channel.Dispatcher.Dispatch(e);
        }

        protected virtual void OnPacketReceived(RTSPPacketReceivedEventArgs e)
        {
            _channel.Dispatcher.Dispatch(e);
        }

        protected virtual void OnError(RTSPErrorEventArgs e)
        {
            _channel.Dispatcher.Dispatch(e);
        }
    }

    public sealed class RTSPTcpMediaTransport : RTSPMediaTransport
    {
        public RTSPTcpMediaTransport(IRTSPMediaChannel channel, IRTSPConnection connection)
            : base( channel , connection )
        {
        }

        public override object SyncRoot
            => throw new NotImplementedException();
        public override bool HasOptions
            => throw new NotImplementedException();
        public override bool IsDescribed
            => throw new NotImplementedException();
        public override bool IsSetup
            => throw new NotImplementedException();
        public override bool IsStarted
            => throw new NotImplementedException();

        public override bool Options()
             => throw new NotImplementedException();
        public override bool Describe()
            => throw new NotImplementedException();
        public override bool Setup()
            => throw new NotImplementedException();
        public override bool Play()
            => throw new NotImplementedException();
        public override void TearDown()
            => throw new NotImplementedException();
        public override void Dispose() 
            => throw new NotImplementedException();

        protected override void OnPacketReceived(RTSPPacketReceivedEventArgs e)
        {
            Channel.Dispatcher.RaiseEvent( e );
        }
    }

    public sealed class RTSPUdpMediaTransport : RTSPMediaTransport
    {
        public RTSPUdpMediaTransport(IRTSPMediaChannel channel, IRTSPConnection connection)
            : base(channel,connection)
        {
        }

        public override object SyncRoot
            => throw new NotImplementedException();
        public override bool HasOptions
            => throw new NotImplementedException();
        public override bool IsDescribed
            => throw new NotImplementedException();
        public override bool IsSetup
            => throw new NotImplementedException();
        public override bool IsStarted
            => throw new NotImplementedException();

        public override bool Options()
             => throw new NotImplementedException();
        public override bool Describe()
            => throw new NotImplementedException();
        public override bool Setup()
            => throw new NotImplementedException();
        public override bool Play()
            => throw new NotImplementedException();
        public override void TearDown()
            => throw new NotImplementedException();
        public override void Dispose()
            => throw new NotImplementedException();
    }

    public sealed class RTSPMulticastMediaTransport : RTSPMediaTransport
    {
        public RTSPMulticastMediaTransport(IRTSPMediaChannel channel, IRTSPConnection connection)
            : base(channel,connection)
        {
        }

        public override object SyncRoot
            => throw new NotImplementedException();
        public override bool HasOptions
            => throw new NotImplementedException();
        public override bool IsDescribed
            => throw new NotImplementedException();
        public override bool IsSetup
            => throw new NotImplementedException();
        public override bool IsStarted
            => throw new NotImplementedException();

        public override bool Options()
             => throw new NotImplementedException();
        public override bool Describe()
             => throw new NotImplementedException();
        public override bool Setup()
            => throw new NotImplementedException();
        public override bool Play()
            => throw new NotImplementedException();
        public override void TearDown()
            => throw new NotImplementedException();
        public override void Dispose()
            => throw new NotImplementedException();
    }

    public sealed class RTSPMediaTransportFactory
    {
        private readonly IRTSPMediaChannel _channel;
        private readonly IRTSPConnection _connection;

        public RTSPMediaTransportFactory( IRTSPMediaChannel channel , IRTSPConnection connection )
        {
            _channel = channel;
            _connection = connection;
        }

        public RTSPMediaTransport CreateMediaTransport()
        {
            if ( _channel.Configuration.DeliveryMode == RTSPDeliveryMode.Udp )
            {
                return new RTSPUdpMediaTransport( _channel , _connection );
            }

            if ( _channel.Configuration.DeliveryMode == RTSPDeliveryMode.Multicast )
            {
                return new RTSPMulticastMediaTransport( _channel , _connection );
            }

            return new RTSPTcpMediaTransport( _channel , _connection );
        }
    }
}