using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace RabbitOM.Player.Controls
{
    public static class DispatcherExtensions
    {
        public static void BeginInvoke( this Dispatcher source , DispatcherPriority priority , Action action )
        {
            Debug.Assert( source != null );
            Debug.Assert( action != null );

            source.BeginInvoke( priority , action );
        }
    }
}
