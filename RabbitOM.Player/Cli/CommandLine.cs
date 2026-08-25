using System;

namespace RabbitOM.Player.Cli
{
    public sealed class CommandLine : ICommandLine
    {
        public ICommandLine AddHandler<TCommand>( ICommandHandler<TCommand> handler ) where TCommand : Command
        {
            throw new NotImplementedException();
        }

        public ICommandLine AddHandler<TCommand>( ICommandHandler<TCommand> handler , Func<TCommand> factory ) where TCommand : Command
        {
            throw new NotImplementedException();
        }

        public void Run( string[] args )
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
