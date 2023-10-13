﻿using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    /// <summary>
    /// Represent the client host
    /// </summary>
    public sealed class RTSPClientSessionHost : IDisposable
    {
        private const int                   PressureLimit   = 3400000;





        private TimeSpan                    _idleTimeout    = TimeSpan.Zero;

        private readonly RTSPClientSession  _session        = null;






        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="session">the session</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPClientSessionHost( RTSPClientSession session )
        {
            _session = session ?? throw new ArgumentNullException( nameof( session ) );
            
            _session.SubscribeEvents();
            GC.AddMemoryPressure(PressureLimit); 
        }







        /// <summary>
        /// Gets the idle timeout
        /// </summary>
        public TimeSpan IdleTimeout
        {
            get => _idleTimeout;
        }








        /// <summary>
        /// Handle the rtsp client connection loop and keep the session active (ping the session by calling a keep alive method)
        /// </summary>
        public void Run()
        {
            if ( ! _session.IsOpened )
            {
                _idleTimeout = _session.Options.RetriesInterval;

                if ( _session.Connect() )
                {
                    _idleTimeout = _session.Options.KeepAliveInterval;
                }
            }
            else
            {
                if ( ! _session.Ping() )
                {
                    _session.Disconnect();
                    
                    _idleTimeout = _session.Options.RetriesInterval;
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
                _session.Disconnect();
            }

            _session.UnSusbcribeEvents();

            GC.RemoveMemoryPressure(PressureLimit);
        }
    }
}
