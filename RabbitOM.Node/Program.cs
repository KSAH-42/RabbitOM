using System;

namespace RabbitOM.Node
{
    using RabbitOM.Node.Application;

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
                var application = new ApplicationBuilder()
                    .SetParameters( args )
                    .LoadScript()
                    .SetupRunner()
                    .Build();

                application.Run();
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
