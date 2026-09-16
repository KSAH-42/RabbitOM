using System;

namespace RabbitOM.NodeShell
{
    using RabbitOM.NodeShell.Services;

	static class Program
    {
        static void Main( string[] args )
        {
            if ( ApplicationHelp.CanShowHelp( args ) )
            {
                ApplicationHelp.ShowHelp();
                return;
            }

            try
            {
                var service = new ApplicationServiceBuilder()
                    .SetParameters( args )
                    .SetupEventDispatcher()
                    .Build();

                service.Run();
            }
            catch( Exception ex )
            {
                ApplicationHelp.ShowHelp( ex );
            }
            finally
            {
                Console.ResetColor();
            }
        }
    }
}
