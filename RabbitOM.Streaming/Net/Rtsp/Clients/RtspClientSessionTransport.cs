using RabbitOM.Streaming.Threading;
using System;

namespace RabbitOM.Streaming.Net.Rtsp.Clients
{
    /// <summary>
    /// Represent the client transport layer used to receive packet from the network
    /// </summary>
    internal abstract class RtspClientSessionTransport
    {
        private readonly BackgroundWorker  _thread      = null;

        private RtspClientSession          _session     = null;

        private TimeSpan                   _idleTimeout = TimeSpan.Zero;
        







        /// <summary>
        /// Constructor
        /// </summary>
        protected RtspClientSessionTransport()
        {
            _thread  = new BackgroundWorker( "Rtsp - Transport session thread" );
        }
        







        /// <summary>
        /// Gets the session
        /// </summary>
        protected RtspClientSession Session
        {
            get => _session;
        }

        /// <summary>
        /// Gets / Sets the idle timeout
        /// </summary>
        protected TimeSpan IdleTimeout
        {
            get => _idleTimeout;
            set => _idleTimeout = value;
        }

        /// <summary>
        /// Check if the transport is active
        /// </summary>
        public bool IsStarted
        {
            get => _thread.IsStarted;
        }
        







        /// <summary>
        /// Start the transport layer
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Start()
        {
            if ( _session == null )
            {
                return false;
            }

            return _thread.Start( () =>
            {
                IdleTimeout = TimeSpan.Zero;

                while ( _thread.CanContinue( IdleTimeout ) )
                {
                    Run();
                }
            } );
        }

        /// <summary>
        /// Stop the transport layer
        /// </summary>
        public void Stop()
        {
            Shutdown();
            _thread.Stop();
        }

        /// <summary>
        /// Execute the custom of the transport layer
        /// </summary>
        protected abstract void Run();

        /// <summary>
        /// Call before the stop 
        /// </summary>
        protected abstract void Shutdown();

        /// <summary>
        /// Set the sesion
        /// </summary>
        /// <param name="session">the session instance</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        public void SetSession( RtspClientSession session )
        {
            if ( _session != null )
            {
                throw new InvalidOperationException( "The session has been already defined" );
            }

            _session = session ?? throw new ArgumentNullException( nameof( session ) );
        }
        







        /// <summary>
        /// Occurs when the transport has received some data
        /// </summary>
        /// <param name="data">the data</param>
        protected virtual void OnDataReceived( byte[] data )
        {
            _session.Dispatcher.DispatchEvent( new RtspPacketReceivedEventArgs( new RtspPacket( data ) ) );
        }
    }
}
