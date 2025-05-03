using System;

namespace RabbitOM.Streaming
{
    /// <summary>
    /// Represent an helper class used to safely invoke delegate
    /// </summary>
    public static class DelegateExtensions
    {
        /// <summary>
        /// Try to invoke
        /// </summary>
        /// <param name="routine">the delegate</param>
        /// <param name="args">the args</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryDynamicInvoke( this Delegate routine , params object[] args )
        {
            if ( routine == null )
            {
                return false;
            }

            try
            {
                routine.DynamicInvoke( args );

                return true;
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }




        /// <summary>
        /// Occurs when an exception is triggered
        /// </summary>
        /// <param name="ex">the exception</param>
        private static void OnException( Exception ex )
        {
            System.Diagnostics.Debug.WriteLine( ex );
        }
    }
}
