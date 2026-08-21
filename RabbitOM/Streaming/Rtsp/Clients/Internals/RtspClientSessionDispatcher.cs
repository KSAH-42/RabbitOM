using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    using RabbitOM.Threading;

    internal sealed class RtspClientSessionDispatcher : IRtspClientEvents
    {
        public event EventHandler<RtspClientCommunicationStartedEventArgs> CommunicationStarted  = null;
        public event EventHandler<RtspClientCommunicationStoppedEventArgs> CommunicationStopped  = null;
        public event EventHandler<RtspClientConnectedEventArgs>            Connected             = null;
        public event EventHandler<RtspClientDisconnectedEventArgs>         Disconnected          = null;
        public event EventHandler<RtspPacketReceivedEventArgs>             PacketReceived        = null;
        public event EventHandler<RtspClientErrorEventArgs>                Error                 = null;


        private readonly BackgroundWorker _eventListener;
        private readonly RtspEventConcurrentQueue  _eventQueue;
        private readonly object _sender;


        internal RtspClientSessionDispatcher( object sender )
        {
            _sender = sender ?? throw new ArgumentNullException( nameof( sender ) );
            _eventListener = new BackgroundWorker("Rtsp - Event listener");
            _eventQueue = new RtspEventConcurrentQueue();
        }



        public bool IsRunning
        {
            get => _eventListener.IsStarted;
        }



        public void Run()
        {
            _eventListener.Start( DoEvents );
        }

        public void Terminate()
        {
            _eventListener.Stop();
            _eventQueue.Clear();
        }

        public void DispatchEvent( EventArgs e )
        {
            _eventQueue.TryEnqueue( e );
        }

        private void DoEvents()
        {
            void PumpEvents()
            {
                while ( _eventQueue.Any() )
                {
                    if ( _eventQueue.TryDequeue( out EventArgs eventArgs ) )
                    {
                        DoDispatch( eventArgs );
                    }
                }
            };

            while ( RtspEventConcurrentQueue.Wait( _eventQueue , _eventListener.ExitHandle ) )
            {
                PumpEvents();
            }

            PumpEvents();
        }

        private void DoDispatch(EventArgs e)
        {
            switch (e)
            {
                case RtspPacketReceivedEventArgs eventArgs:
                    OnPacketReceived(eventArgs);
                    break;

                case RtspClientConnectedEventArgs eventArgs:
                    OnConnected(eventArgs);
                    break;

                case RtspClientDisconnectedEventArgs eventArgs:
                    OnDisconnected(eventArgs);
                    break;

                case RtspClientCommunicationStartedEventArgs eventArgs:
                    OnCommunicationStarted(eventArgs);
                    break;

                case RtspClientCommunicationStoppedEventArgs eventArgs:
                    OnCommunicationStopped(eventArgs);
                    break;

                case RtspClientErrorEventArgs eventArgs:
                    OnError(eventArgs);
                    break;
            }
        }



        private void OnCommunicationStarted( RtspClientCommunicationStartedEventArgs e )
        {
            CommunicationStarted?.TryInvoke(_sender , e );
        }

        private void OnCommunicationStopped( RtspClientCommunicationStoppedEventArgs e )
        {
            CommunicationStopped?.Invoke(_sender , e );
        }

        private void OnConnected( RtspClientConnectedEventArgs e )
        {
            Connected?.TryInvoke(_sender , e );
        }

        private void OnDisconnected( RtspClientDisconnectedEventArgs e )
        {
            Disconnected?.TryInvoke(_sender , e );
        }

        private void OnPacketReceived(RtspPacketReceivedEventArgs e )
        {
            PacketReceived?.TryInvoke(_sender , e );
        }

        private void OnError( RtspClientErrorEventArgs e )
        {
            Error?.TryInvoke( _sender , e );
        }
    }
}
