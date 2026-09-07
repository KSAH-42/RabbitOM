using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace RabbitOM.Player.Controls
{
    public static class DispatcherExtensions
    {
        public static void BeginFastInvoke( this Dispatcher source , Action action )
        {
            Debug.Assert( source != null );
            Debug.Assert( action != null );

            source.BeginInvoke( DispatcherPriority.Render , action );
        }
    }
}
