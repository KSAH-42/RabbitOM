using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent an helper class used to safely invoke event handler multicast delegate
    /// </summary>
    public static class RTSPEventHandlerExtensions
    {
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
            if ( sender == null || e == null || handler == null )
            {
                return false;
            }

            try
            {
                handler.Invoke( sender , e );

                return true;
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return false;
        }

        /// <summary>
        /// Raise an event handler
        /// </summary>
        /// <typeparam name="TEventArg">the type of event args</typeparam>
        /// <param name="e">the event args</param>
        /// <param name="handler">the delegate</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryInvoke<TEventArg>( this Action<TEventArg> handler  , TEventArg e )
            where TEventArg : EventArgs
        {
            if ( e == null || handler == null )
            {
                return false;
            }

            try
            {
                handler.Invoke( e );

                return true;
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return false;
        }
    }
}
