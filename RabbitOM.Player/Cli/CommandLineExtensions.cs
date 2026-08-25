using System;

namespace RabbitOM.Player.Cli
{
    public static class CommandLineExtensions
    {
        public static ICommandLine AddHandler<TCommand>( this ICommandLine commandLine , Action<TCommand> handler ) where TCommand : Command
        {
            if ( commandLine == null )
            {
                throw new ArgumentNullException( nameof( commandLine ) );
            }

            return commandLine.AddHandler( new RelayCommandHandler<TCommand>( handler ) );
        }

        public static ICommandLine AddHandler<TCommand>( this ICommandLine commandLine , Action<TCommand> handler , Func<TCommand> factory ) where TCommand : Command
        {
            if ( commandLine == null )
            {
                throw new ArgumentNullException( nameof( commandLine ) );
            }

            return commandLine.AddHandler( new RelayCommandHandler<TCommand>( handler ) , factory );
        }
    }
}
