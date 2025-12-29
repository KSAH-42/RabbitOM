// TODO: Refactor and used immutable type for configuration, and use constructor injection
// TODO: removing the lock ??

using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    /// <summary>
    /// Represent the base frame builder class
    /// </summary>
    public abstract class RtpFrameBuilder : IDisposable
    {
        /// <summary>
        /// Raised when a complete frame has been received
        /// </summary>
        public event EventHandler<RtpFrameReceivedEventArgs> FrameReceived;





        private readonly object _lock = new object();





        /// <summary>
        /// Finalizer
        /// </summary>
        ~RtpFrameBuilder()
        {
            Dispose( false );
        }






        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _lock;
        }






        /// <summary>
        /// Setup - Generally called when the builder has entierely configured and before the streaming
        /// </summary>
        public virtual void Setup() { }

        /// <summary>
        /// Write a buffer
        /// </summary>
        /// <param name="buffer">the buffer</param>
        public abstract void Write( byte[] buffer );

        /// <summary>
        /// Clear generally used to reset the builder
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            lock ( _lock )
            {
                Dispose( true );
            }

            GC.SuppressFinalize( this );
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">true when the dispose method is explicity called</param>
        protected virtual void Dispose( bool disposing )
        {
        }






        /// <summary>
        /// Fire a frame event received
        /// </summary>
        /// <param name="e">the event to fired outside</param>
        protected virtual void OnFrameReceived( RtpFrameReceivedEventArgs e )
        {
            FrameReceived?.TryInvoke( this , e );
        }
    }
}