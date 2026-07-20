using System;

namespace RabbitOM
{
    public static class DelegateExtensions
    {
        public static bool TryDynamicInvoke( this Delegate routine , params object[] args )
        {
            if ( routine == null )
            {
                return false;
            }

            try
            {
                routine.DynamicInvoke( args );

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
