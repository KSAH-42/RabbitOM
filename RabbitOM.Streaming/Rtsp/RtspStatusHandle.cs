using System;
using System.Threading;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a safe event handle class
    /// </summary>
    internal sealed class RtspStatusHandle
    {
        private readonly EventWaitHandle _handle = new ManualResetEvent( false );

        /// <summary>
        /// Check if the status has been activated
        /// </summary>
        public bool Value
        {
            get => _handle.TryWait( TimeSpan.Zero );
        }

        /// <summary>
        /// Activate the event
        /// </summary>
        public void Activate()
        {
            _handle.TrySet();
        }

        /// <summary>
        /// Deactivate
        /// </summary>
        public void Deactivate()
        {
            _handle.TryReset();
        }

        /// <summary>
        /// Wait the activation
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Wait( TimeSpan timeout )
        {
            return _handle.TryWait( timeout );
        }
    }
}
