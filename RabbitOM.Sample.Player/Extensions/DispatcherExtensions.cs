using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace RabbitOM.Sample.Player.Extensions
{
    internal static class DispatcherExtensions
    {
        // we don't use the pattern try to avoid return bool a create copy on the stack because we stream
        public static void BeginSafeInvoke( this Dispatcher dispatcher , DispatcherPriority priority , Action action )
        {
            Debug.Assert( dispatcher != null );

            dispatcher.BeginInvoke( priority , new Action( () =>
            {
                action.TryInvoke();
            } ) );
        }
    }
}
