using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    /// <summary>
    /// Represent the event dispatcher
    /// </summary>
    internal sealed class RTSPClientSessionDispatcher : IRTSPClientEvents
    {
        /// <summary>
        /// Raised when the communication has been started
        /// </summary>
        public event EventHandler<RTSPClientCommunicationStartedEventArgs> CommunicationStarted  = null;

        /// <summary>
        /// Raised when the communication has been stopped
        /// </summary>
        public event EventHandler<RTSPClientCommunicationStoppedEventArgs> CommunicationStopped  = null;

        /// <summary>
        /// Raised when the client is connected
        /// </summary>
        public event EventHandler<RTSPClientConnectedEventArgs>            Connected             = null;

        /// <summary>
        /// Raised when the client is disconnected
        /// </summary>
        public event EventHandler<RTSPClientDisconnectedEventArgs>         Disconnected          = null;

        /// <summary>
        /// Raise when an data has been received
        /// </summary>
        public event EventHandler<RTSPPacketReceivedEventArgs>             PacketReceived        = null;

        /// <summary>
        /// Raise when an error occurs
        /// </summary>
        public event EventHandler<RTSPClientErrorEventArgs>                Error                 = null;


        


        private readonly RTSPThread     _eventListener       = new RTSPThread( "RTSP - Event listener" );

        private readonly RTSPEventQueue _eventQueue          = new RTSPEventQueue();
        

        


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
            _eventListener.Start( () =>
            {
                while ( WaitEvents() )
                {
                    DoEvents();
                }

                DoEvents();
            } );
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
        public void DispatchEvent( RTSPClientCommunicationStartedEventArgs e )
        {
            _eventQueue.Enqueue( e );
        }

        /// <summary>
        /// Dispatch an event
        /// </summary>
        /// <param name="e">the event</param>
        public void DispatchEvent( RTSPClientCommunicationStoppedEventArgs e )
        {
            _eventQueue.Enqueue( e );
        }

        /// <summary>
        /// Dispatch an event
        /// </summary>
        /// <param name="e">the event</param>
        public void DispatchEvent( RTSPClientConnectedEventArgs e )
        {
            _eventQueue.Enqueue( e );
        }

        /// <summary>
        /// Dispatch an event
        /// </summary>
        /// <param name="e">the event</param>
        public void DispatchEvent( RTSPClientDisconnectedEventArgs e )
        {
            _eventQueue.Enqueue( e );
        }

        /// <summary>
        /// Dispatch an event
        /// </summary>
        /// <param name="e">the event</param>
        public void DispatchEvent(RTSPPacketReceivedEventArgs e )
        {
            _eventQueue.Enqueue( e );
        }

        /// <summary>
        /// Dispatch an event
        /// </summary>
        /// <param name="e">the event</param>
        public void DispatchEvent( RTSPClientErrorEventArgs e )
        {
            _eventQueue.Enqueue( e );
        }


        


        /// <summary>
        /// Wait events
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        private bool WaitEvents()
        {
            return RTSPEventQueue.Wait( _eventQueue , _eventListener.ExitHandle );
        }

        /// <summary>
        /// Pump event
        /// </summary>
        private void DoEvents()
        {
            while ( _eventQueue.Any() )
            {
                if ( _eventQueue.TryDequeue( out EventArgs eventArgs ) )
                {
                    DoDispatch( eventArgs );
                }
            }
        }

        /// <summary>
        /// Handle the event
        /// </summary>
        /// <param name="e">the event arg</param>
        private void DoDispatch(EventArgs e)
        {
            switch (e)
            {
                case RTSPPacketReceivedEventArgs eventArgs:
                    OnPacketReceived(eventArgs);
                    break;

                case RTSPClientConnectedEventArgs eventArgs:
                    OnConnected(eventArgs);
                    break;

                case RTSPClientDisconnectedEventArgs eventArgs:
                    OnDisconnected(eventArgs);
                    break;

                case RTSPClientCommunicationStartedEventArgs eventArgs:
                    OnCommunicationStarted(eventArgs);
                    break;

                case RTSPClientCommunicationStoppedEventArgs eventArgs:
                    OnCommunicationStopped(eventArgs);
                    break;

                case RTSPClientErrorEventArgs eventArgs:
                    OnError(eventArgs);
                    break;
            }
        }







        /// <summary>
        /// Occurs when the communication has been started
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnCommunicationStarted( RTSPClientCommunicationStartedEventArgs e )
        {
            CommunicationStarted?.TryInvoke( this , e );
        }

        /// <summary>
        /// Occurs when the communication has been stopped
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnCommunicationStopped( RTSPClientCommunicationStoppedEventArgs e )
        {
            CommunicationStopped?.Invoke( this , e );
        }

        /// <summary>
        /// Occurs when the client is connected
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnConnected( RTSPClientConnectedEventArgs e )
        {
            Connected?.TryInvoke( this, e );
        }

        /// <summary>
        /// Occurs when the client has been disconnected
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnDisconnected( RTSPClientDisconnectedEventArgs e )
        {
            Disconnected?.TryInvoke( this, e );
        }

        /// <summary>
        /// Occurs when data has been received
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnPacketReceived(RTSPPacketReceivedEventArgs e )
        {
            PacketReceived?.TryInvoke( this, e );
        }

        /// <summary>
        /// Occurs when an error has been detected
        /// </summary>
        /// <param name="e">the event args</param>
        private void OnError( RTSPClientErrorEventArgs e )
        {
            Error?.TryInvoke( this, e );
        }
    }
}
