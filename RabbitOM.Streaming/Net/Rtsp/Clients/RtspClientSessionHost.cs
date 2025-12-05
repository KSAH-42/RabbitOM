using System;

namespace RabbitOM.Streaming.Net.Rtsp.Clients
{
    /// <summary>
    /// Represent the client host
    /// </summary>
    internal sealed class RtspClientSessionHost : IDisposable
    {
        private TimeSpan                    _idleTimeout    = TimeSpan.Zero;

        private readonly RtspClientSession  _session        = null;






        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="session">the session</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspClientSessionHost( RtspClientSession session )
        {
            _session = session ?? throw new ArgumentNullException( nameof( session ) );
            
            _session.SubscribeEvents();
        }







        /// <summary>
        /// Gets the idle timeout
        /// </summary>
        public TimeSpan IdleTimeout
        {
            get => _idleTimeout;
        }








        /// <summary>
        /// Handle the Rtsp client connection loop and keep the session active (ping the session by calling a keep alive method)
        /// </summary>
        public void Run()
        {
            if ( ! _session.IsOpened )
            {
                _idleTimeout = _session.Configuration.RetriesInterval;

                if ( _session.Open() )
                {
                    _idleTimeout = _session.Configuration.KeepAliveInterval;
                }
            }
            else
            {
                if ( ! _session.Ping() )
                {
                    _session.Close();
                    
                    _idleTimeout = _session.Configuration.RetriesInterval;
                }
            }
        }

        /// <summary>
        /// Release internal resources
        /// </summary>
        public void Dispose()
        {
            if (_session.IsOpened)
            {
                _session.Close();
            }

            _session.UnSusbcribeEvents();
        }
    }
}
