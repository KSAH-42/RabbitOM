using RabbitOM.Streaming.Collections;
using RabbitOM.Streaming.Threading;
using System;

namespace RabbitOM.Streaming.Net.Rtsp.Clients
{
    /// <summary>
    /// Represent the event dispatcher
    /// </summary>
    internal sealed class RtspClientSessionDispatcher : IRtspClientEvents
    {
        /// <summary>
        /// Raised when the communication has been started
        /// </summary>
        public event EventHandler<RtspClientCommunicationStartedEventArgs> CommunicationStarted  = null;

        /// <summary>
        /// Raised when the communication has been stopped
        /// </summary>
        public event EventHandler<RtspClientCommunicationStoppedEventArgs> CommunicationStopped  = null;

        /// <summary>
        /// Raised when the client is connected
        /// </summary>
        public event EventHandler<RtspClientConnectedEventArgs>            Connected             = null;

        /// <summary>
        /// Raised when the client is disconnected
        /// </summary>
        public event EventHandler<RtspClientDisconnectedEventArgs>         Disconnected          = null;

        /// <summary>
        /// Raise when an data has been received
        /// </summary>
        public event EventHandler<RtspPacketReceivedEventArgs>             PacketReceived        = null;

        /// <summary>
        /// Raise when an error occurs
        /// </summary>
        public event EventHandler<RtspClientErrorEventArgs>                Error                 = null;


        


        private readonly BackgroundWorker      _eventListener;

        private readonly EventConcurrentQueue  _eventQueue;

        private readonly object                _sender;




        /// <summary>
        /// Initialize a new instance of the dispatcher
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <exception cref="ArgumentNullException"/>
        internal RtspClientSessionDispatcher( object sender )
        {
            _sender = sender ?? throw new ArgumentNullException( nameof( sender ) );
            _eventListener = new BackgroundWorker("Rtsp - Event listener");
            _eventQueue = new EventConcurrentQueue();
        }
        

        


        /// <summary>
        /// Gets a status that tels if the underlaying listener are running
        /// </summary>
        public bool IsRunning
        {
            get => _eventListener.IsStarted;
        }
 

        


        /// <summary>
        /// Start the event listening
        /// </summary>
        public void Run()
        {
            _eventListener.Start( DoEvents );
        }

        /// <summary>
        /// Stop the event listening
        /// </summary>
        public void Terminate()
        {
            _eventListener.Stop();
            _eventQueue.Clear();
        }

        /// <summary>
        /// Dispatch an event
        /// </summary>
        /// <param name="e">the event</param>
        public void DispatchEvent( EventArgs e )
        {
            _eventQueue.TryEnqueue( e );
        }


        


        /// <summary>
        /// Pump event
        /// </summary>
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

            while ( EventConcurrentQueue.Wait( _eventQueue , _eventListener.ExitHandle ) )
            {
                PumpEvents();
            }

            PumpEvents();
        }

        /// <summary>
        /// Handle the event
        /// </summary>
        /// <param name="e">the event arg</param>
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







        /// <summary>
        /// Occurs when the communication has been started
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnCommunicationStarted( RtspClientCommunicationStartedEventArgs e )
        {
            CommunicationStarted?.TryInvoke(_sender , e );
        }

        /// <summary>
        /// Occurs when the communication has been stopped
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnCommunicationStopped( RtspClientCommunicationStoppedEventArgs e )
        {
            CommunicationStopped?.Invoke(_sender , e );
        }

        /// <summary>
        /// Occurs when the client is connected
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnConnected( RtspClientConnectedEventArgs e )
        {
            Connected?.TryInvoke(_sender , e );
        }

        /// <summary>
        /// Occurs when the client has been disconnected
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnDisconnected( RtspClientDisconnectedEventArgs e )
        {
            Disconnected?.TryInvoke(_sender , e );
        }

        /// <summary>
        /// Occurs when data has been received
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnPacketReceived(RtspPacketReceivedEventArgs e )
        {
            PacketReceived?.TryInvoke(_sender , e );
        }

        /// <summary>
        /// Occurs when an error has been detected
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnError( RtspClientErrorEventArgs e )
        {
            Error?.TryInvoke( _sender , e );
        }
    }
}
