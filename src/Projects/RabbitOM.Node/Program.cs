using System;

namespace RabbitOM.Node
{
    using RabbitOM.Node.Application;

    // Create a RabbitOM.Player.Node.exe that don't used the rtsp client must just run a script like a batch the player can download the script somewhere, to be explore.... 
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
                    .SetupScriptHost()
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
