using System;

namespace RabbitOM.Player.Cli
{
    [Command( "help" )]
    [CommandType( "-h" )]
    [CommandType( "--help" )]
    [CommandType( "-?" )]
    [CommandType( "/?" )]
    public sealed class ShowHelpCommand : Command
    {
    }
}
