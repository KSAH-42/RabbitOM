using System;

namespace RabbitOM.Node
{
    using RabbitOM.Node.Application;

	static class Program
    {
        static void Main( string[] args )
        {
            if ( HelpManager.CanShowHelp( args ) )
            {
                HelpManager.ShowHelp();
                return;
            }

            try
            {
                var application = new ApplicationBuilder( ApplicationSettings.Parse( args ) )
                    .LoadScript()
                    .SetupRunner()
                    .Build()
                    ;

                application.Run();
            }
            catch( Exception ex )
            {
                HelpManager.ShowHelp( ex );
            }
            finally
            {
                Console.ResetColor();
            }
        }
    }
}
