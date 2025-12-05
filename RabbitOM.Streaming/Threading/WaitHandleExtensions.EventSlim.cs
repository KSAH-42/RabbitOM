using System;
using System.Threading;

namespace RabbitOM.Streaming.Threading
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
        public static bool TrySet( this ManualResetEventSlim handle )
        {
            if ( handle == null )
			{
                return false;
			}

            try
            {
                handle.Set();

                return true;
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
        public static bool TryReset( this ManualResetEventSlim handle )
        {
            if ( handle == null )
			{
                return false;
			}

            try
            {
                handle.Reset();
                return true;
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
        public static bool TryWait( this ManualResetEventSlim handle )
        {
            try
            {
                return handle?.Wait( Timeout.Infinite ) ?? false;
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
        public static bool TryWait( this ManualResetEventSlim handle , WaitHandle cancellationHandle )
        {
            if ( handle == null || cancellationHandle == null )
            {
                return false;
            }

            try
            {
                var handles = new WaitHandle[]
                {
                    cancellationHandle , handle.WaitHandle
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
        public static bool TryWait( this ManualResetEventSlim handle , int timeout )
        {
            try
            {
                return handle?.Wait( timeout ) ?? false;
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
        public static bool TryWait( this ManualResetEventSlim handle , TimeSpan timeout )
        {
            try
            {
                return handle?.Wait( timeout ) ?? false;
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
        public static bool TryWait( this ManualResetEventSlim handle , int timeout , WaitHandle cancellationHandle )
        {
            if ( handle == null || cancellationHandle == null )
            {
                return false;
            }

            try
            {
                var handles = new WaitHandle[]
                {
                    cancellationHandle , handle.WaitHandle
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
        public static bool TryWait( this ManualResetEventSlim handle , TimeSpan timeout , WaitHandle cancellationHandle )
        {
            if ( handle == null || cancellationHandle == null )
            {
                return false;
            }

            try
            {
                var handles = new WaitHandle[]
                {
                    cancellationHandle , handle.WaitHandle
                };

                return WaitHandle.WaitAny( handles , timeout ) == 1;
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }
    }
}
