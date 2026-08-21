using System;
using System.Diagnostics;

namespace RabbitOM.Player.Cli
{
    public static class CommandFactory
    {
        public static bool TryCreateCommand( Type type , out Command result )
        {
            result = null;

            if ( type == null )
            {
                return false;
            }

            try
            {
                result = Activator.CreateInstance( type ) as Command;
                return result != null;
            }
            catch( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        private static void OnError( Exception ex )
        {
           Debug.WriteLine( ex );
        }
    }
}
