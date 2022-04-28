using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent an helper class used to safely invoke event handler multicast delegate
    /// </summary>
    public static class RTSPEventInvoker
    {
        /// <summary>
        /// Raise an event handler
        /// </summary>
        /// <typeparam name="TEventArg">the type of event args</typeparam>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event args</param>
        /// <param name="handler">the delegate</param>
        public static void RaiseEvent<TEventArg>( object sender , TEventArg e , EventHandler<TEventArg> handler )
            where TEventArg : EventArgs
        {
            if ( sender == null || e == null || handler == null )
            {
                return;
            }

            try
            {
                handler.Invoke( sender , e );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }
        }

        /// <summary>
        /// Raise an event handler
        /// </summary>
        /// <typeparam name="TEventArg">the type of event args</typeparam>
        /// <param name="e">the event args</param>
        /// <param name="handler">the delegate</param>
        public static void RaiseCallback<TEventArg>( TEventArg e , Action<TEventArg> handler )
            where TEventArg : EventArgs
        {
            if ( e == null || handler == null )
            {
                return;
            }

            try
            {
                handler.Invoke( e );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }
        }
    }
}
