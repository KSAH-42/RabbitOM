using System;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    internal sealed class RtspClientSessionHost : IDisposable
    {
        private TimeSpan _idleTimeout;

        private readonly RtspSession _session;

        public RtspClientSessionHost( RtspSession session )
        {
            _session = session ?? throw new ArgumentNullException( nameof( session ) );
            _session.SubscribeEvents();
        }

        public TimeSpan IdleTimeout
        {
            get => _idleTimeout;
        }

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
