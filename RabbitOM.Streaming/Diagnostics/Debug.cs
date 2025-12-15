using System;
using System.Diagnostics;

namespace RabbitOM.Streaming.Diagnostics
{
    public static class Debug
    {
        [Conditional( "DEBUG" )]
        public static void EnsureCondition( Func<bool> condition , string message = null )
        {
            if ( condition == null )
            {
                throw new ArgumentNullException( nameof( condition ) );
            }

            if ( ! condition() )
            {
                throw new InvalidOperationException( message );
            }
        }
    }
}
