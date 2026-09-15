// TODO: rename as RabbitOM.Node
// TODO: then remove the code below
// TODO: then add the cli
// TODO: then add decoders and enable or not decoding using the cli
// TODO: then add the interpertor
// TODO: add socket redirector
// TODO: add storage
// TODO: migrate using asp .net core and move as RabbitOM.Node.Api
// TODO: add the RabbitOM.Node.Front as web interface that interact with backend

// The Player Cli will used when it's implemented will be finished, here we don't used a cli.

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
                    .AddParameters( args )
                    .AddEventDispatcher()
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
