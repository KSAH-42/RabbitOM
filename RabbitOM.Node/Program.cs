using System;

namespace RabbitOM.Node
{
    using RabbitOM.Node.Services;

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
                    .LoadScript()
                    .SetupRunner()
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
