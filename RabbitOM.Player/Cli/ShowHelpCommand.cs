using System;

namespace RabbitOM.Player.Cli
{
    [Command( "help" )]
    [CommandType( "-h" )]
    [CommandType( "--help" )]
    public sealed class ShowHelpCommand : Command
    {
        private readonly IApplication _application;

        public ShowHelpCommand( IApplication application )
        {
            _application = application ?? throw new ArgumentNullException( nameof( application ) );
        }

        public override void Execute()
        {
            _application.ShowHelp();
        }
    }
}
