using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a safe action invoker used by a dispatcher
    /// </summary>
    public sealed class RTSPDispatcherInvoker
    {
        /// <summary>
        /// Invoke the action
        /// </summary>
        /// <param name="action">the action</param>
        public void Invoke( Action action )
        {
            if ( action == null )
            {
                return;
            }

            action();
        }

        /// <summary>
        /// Invoke the action
        /// </summary>
        /// <param name="action">the action</param>
        public bool TryInvoke( Action action )
        {
            if ( action == null )
            {
                return false;
            }

            try
            {
                action();
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return false;
        }
    }
}
