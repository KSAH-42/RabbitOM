﻿using System;

namespace RabbitOM.Streaming.Rtp.Framing
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
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose( true );
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
        /// Clear generally used to reset the builder
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Write a buffer
        /// </summary>
        /// <param name="buffer">the buffer</param>
        public abstract void Write( byte[] buffer );






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