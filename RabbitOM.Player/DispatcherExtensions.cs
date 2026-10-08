using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace RabbitOM.Player
{
    public static class DispatcherExtensions
    {
        public static DispatcherOperation BeginInvoke( this Dispatcher source , DispatcherPriority priority , Action action )
        {
            Debug.Assert( source != null );
            Debug.Assert( action != null );

            return source.BeginInvoke( priority , action );
        }
    }
}
