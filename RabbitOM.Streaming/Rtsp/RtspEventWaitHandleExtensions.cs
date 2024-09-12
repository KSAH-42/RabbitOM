using System;
using System.Threading;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent an extension class
    /// </summary>
    public static class RtspEventWaitHandleExtensions
    {
        /// <summary>
        /// Try to set
        /// </summary>
        /// <param name="handle">the handle</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TrySet( this EventWaitHandle handle )
        {
            try
            {
                return handle?.Set() ?? false;
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Try to reset
        /// </summary>
        /// <param name="handle">the handle</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryReset( this EventWaitHandle handle )
        {
            try
            {
                return handle?.Reset() ?? false;
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Try to wait the activation
        /// </summary>
        /// <param name="handle">the handle</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryWait( this EventWaitHandle handle )
        {
            try
            {
                return handle?.WaitOne() ?? false;
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Try to wait the activation
        /// </summary>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryWait( this EventWaitHandle handle , EventWaitHandle cancellationHandle )
        {
            if ( handle == null || cancellationHandle == null )
            {
                return false;
            }

            try
            {
                var handles = new EventWaitHandle[]
                {
                    cancellationHandle , handle
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
        /// Try to wait the activation
        /// </summary>
        /// <param name="handle">the handle</param>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryWait( this EventWaitHandle handle , int timeout )
        {
            try
            {
                return handle?.WaitOne( timeout ) ?? false;
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Try to wait the activation
        /// </summary>
        /// <param name="handle">the handle</param>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryWait( this EventWaitHandle handle , TimeSpan timeout )
        {
            try
            {
                return handle?.WaitOne( timeout ) ?? false;
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Try wait until an element has been push to the queue
        /// </summary>
        /// <param name="handle">the handle</param>
        /// <param name="timeout">the timeout</param>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryWait( this EventWaitHandle handle , int timeout , EventWaitHandle cancellationHandle )
        {
            if ( handle == null || cancellationHandle == null )
            {
                return false;
            }

            try
            {
                var handles = new EventWaitHandle[]
                {
                    cancellationHandle , handle
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
        /// Try wait until an element has been push to the queue
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryWait( this EventWaitHandle handle , TimeSpan timeout , EventWaitHandle cancellationHandle )
        {
            if ( handle == null || cancellationHandle == null )
            {
                return false;
            }

            try
            {
                var handles = new EventWaitHandle[]
                {
                    cancellationHandle , handle
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
        /// Fired when an exception occurs
        /// </summary>
        /// <param name="ex">the exception</param>
        private static void OnException( Exception ex )
        {
            if ( ex == null )
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine( ex );
        }
    }
}
