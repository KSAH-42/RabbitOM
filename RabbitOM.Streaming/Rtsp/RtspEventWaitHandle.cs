﻿using System;
using System.Threading;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a safe event handle class
    /// </summary>
    public sealed class RtspEventWaitHandle : IDisposable
    {
        private readonly EventWaitHandle _handle = null;




        /// <summary>
        /// Constructor
        /// </summary>
        public RtspEventWaitHandle()
        {
            _handle = new EventWaitHandle( false , EventResetMode.ManualReset );
        }






        /// <summary>
        /// Activate the event
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Set()
        {
            try
            {
                return _handle.Set();
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Deactivate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Reset()
        {
            try
            {
                return _handle.Reset();
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Wait the activation
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Wait()
        {
            try
            {
                return _handle.WaitOne();
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Wait the activation
        /// </summary>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Wait( RtspEventWaitHandle cancellationHandle )
        {
            if ( cancellationHandle == null )
            {
                return false;
            }

            try
            {
                var handles = new EventWaitHandle[]
                {
                    cancellationHandle._handle , _handle
                };

                return EventWaitHandle.WaitAny( handles ) == 1;
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Wait the activation
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Wait( int timeout )
        {
            try
            {
                return _handle.WaitOne( timeout );
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Wait the activation
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Wait( TimeSpan timeout )
        {
            try
            {
                return _handle.WaitOne( timeout );
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Wait( int timeout , RtspEventWaitHandle cancellationHandle )
        {
            if ( cancellationHandle == null )
            {
                return false;
            }

            try
            {
                var handles = new EventWaitHandle[]
                {
                    cancellationHandle._handle , _handle
                };

                return EventWaitHandle.WaitAny( handles , timeout ) == 1;
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Wait( TimeSpan timeout , RtspEventWaitHandle cancellationHandle )
        {
            if ( cancellationHandle == null )
            {
                return false;
            }

            try
            {
                var handles = new EventWaitHandle[]
                {
                    cancellationHandle._handle , _handle
                };

                return EventWaitHandle.WaitAny( handles , timeout ) == 1;
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Release internal resources
        /// </summary>
        public void Dispose()
        {
            this._handle.Dispose();
        }






        /// <summary>
        /// Fired when an exception occurs
        /// </summary>
        /// <param name="ex">the exception</param>
        private void OnException( Exception ex )
        {
            if ( ex == null )
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine( ex );
        }
    }
}
