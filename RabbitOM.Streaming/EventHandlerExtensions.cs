using System;

namespace RabbitOM.Streaming
{
    /// <summary>
    /// Represent an helper class used to safely invoke event handler multicast delegate
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// Raise an event handler
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        /// <param name="handler">the delegate</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryInvoke(this EventHandler handler, object sender, EventArgs e)
        {
            return TryDynamicInvoke( handler , sender ,e );
        }

        /// <summary>
        /// Raise an event handler
        /// </summary>
        /// <typeparam name="TEventArg">the type of event args</typeparam>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        /// <param name="handler">the delegate</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryInvoke<TEventArg>( this EventHandler<TEventArg> handler , object sender , TEventArg e )
            where TEventArg : EventArgs
        {
            return TryDynamicInvoke( handler , sender ,e );
        }

        /// <summary>
        /// Try to invoke a multicast delegate
        /// </summary>
        /// <param name="handler">the delegate</param>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        /// <returns>returns true for a success, otherwise false</returns>
        private static bool TryDynamicInvoke( Delegate handler, object sender, EventArgs e)
        {
            if ( sender == null || e == null || handler == null )
            {
                return false;
            }

            var invokers = handler.GetInvocationList();

            if ( invokers.Length == 0 )
            {
                return false;
            }

            object[] args = new object[] { sender, e };

            bool result = false;

            foreach ( var invoker in invokers )
            {
                try
                {
                    invoker.DynamicInvoke( args );

                    result = true;
                }
                catch ( Exception ex )
                {
                    OnException( ex );
                }
            }
     
            return result;
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
