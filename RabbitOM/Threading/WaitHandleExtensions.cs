using System;
using System.Threading;

namespace RabbitOM.Threading
{
    public static partial class WaitHandleExtensions
    {
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





        private static void OnException( Exception ex )
        {
            System.Diagnostics.Debug.WriteLine( ex );
        }
    }
}
