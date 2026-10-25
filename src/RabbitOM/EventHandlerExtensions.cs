using System;

namespace RabbitOM
{
    public static class EventHandlerExtensions
    {
        public static bool TryInvoke(this EventHandler handler, object sender, EventArgs e)
        {
            return TryDynamicInvoke( handler , sender ,e );
        }

        public static bool TryInvoke<TEventArg>( this EventHandler<TEventArg> handler , object sender , TEventArg e )
            where TEventArg : EventArgs
        {
            return TryDynamicInvoke( handler , sender ,e );
        }

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

            var args = new object[] { sender, e };

            bool result = false;

            foreach ( var invoker in invokers )
            {
                result |= invoker.TryDynamicInvoke( args );
            }

            return result;
        }
    }
}
