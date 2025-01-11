using System;

namespace RabbitOM.Streaming
{
    /// <summary>
    /// Represent an helper class used to safely invoke event handler multicast delegate
    /// </summary>
    public static class ActionExtensions
    {
        /// <summary>
        /// Try to invoke an action
        /// </summary>
        /// <param name="action">the action</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryInvoke( this Action action )
        {
            if ( action == null )
            {
                return false;
            }

            try
            {
                action.Invoke();

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
