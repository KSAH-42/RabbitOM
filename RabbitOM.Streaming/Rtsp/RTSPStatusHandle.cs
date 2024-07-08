using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a safe event handle class
    /// </summary>
    internal sealed class RTSPStatusHandle
    {
        private readonly RTSPEventWaitHandle _handle = new RTSPEventWaitHandle();

        /// <summary>
        /// Check if the status has been activated
        /// </summary>
        public bool Value
        {
            get => _handle.Wait( TimeSpan.Zero );
        }

        /// <summary>
        /// Activate the event
        /// </summary>
        public void Activate()
        {
            _handle.Set();
        }

        /// <summary>
        /// Deactivate
        /// </summary>
        public void Deactivate()
        {
            _handle.Reset();
        }

        /// <summary>
        /// Wait the activation
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Wait( TimeSpan timeout )
        {
            return _handle.Wait( timeout );
        }
    }
}
