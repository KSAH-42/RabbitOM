using System;
using System.Threading;

namespace RabbitOM.Streaming
{
    /// <summary>
    /// Represent an extension class
    /// </summary>
    public static partial class WaitHandleExtensions
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
        public static bool TryWait( this WaitHandle handle )
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
        public static bool TryWait( this WaitHandle handle , WaitHandle cancellationHandle )
        {
            if ( handle == null || cancellationHandle == null )
            {
                return false;
            }

            try
            {
                var handles = new WaitHandle[]
                {
                    cancellationHandle , handle
                };

                return WaitHandle.WaitAny( handles ) == 1;
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
        public static bool TryWait( this WaitHandle handle , int timeout )
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
        public static bool TryWait( this WaitHandle handle , TimeSpan timeout )
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
        public static bool TryWait( this WaitHandle handle , int timeout , WaitHandle cancellationHandle )
        {
            if ( handle == null || cancellationHandle == null )
            {
                return false;
            }

            try
            {
                var handles = new WaitHandle[]
                {
                    cancellationHandle , handle
                };

                return WaitHandle.WaitAny( handles , timeout ) == 1;
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
        public static bool TryWait( this WaitHandle handle , TimeSpan timeout , WaitHandle cancellationHandle )
        {
            if ( handle == null || cancellationHandle == null )
            {
                return false;
            }

            try
            {
                var handles = new WaitHandle[]
                {
                    cancellationHandle , handle
                };

                return WaitHandle.WaitAny( handles , timeout ) == 1;
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        /// <summary>
        /// Get the actual status
        /// </summary>
        /// <param name="handle">the handle</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool IsSet( this WaitHandle handle )
        {
            try
            {
                return handle?.WaitOne( 0 ) ?? false;
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
            System.Diagnostics.Debug.WriteLine( ex );
        }
    }
}
