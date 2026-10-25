using System;

namespace RabbitOM
{
    public static class ActionExtensions
    {
        public static bool TryInvoke( this Action action )
        {
            if ( action == null )
            {
                return false;
            }

            try
            {
                action.Invoke();

                return true;
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            return false;
        }

        private static void OnException( Exception ex )
        {
            System.Diagnostics.Debug.WriteLine( ex );
        }
    }
}
