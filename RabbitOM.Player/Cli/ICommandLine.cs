using System;

namespace RabbitOM.Player.Cli
{
    public interface ICommandLine : IDisposable
    {
        ICommandLine AddHandler<TCommand>( ICommandHandler<TCommand> handler ) where TCommand : Command;

        ICommandLine AddHandler<TCommand>( ICommandHandler<TCommand> handler , Func<TCommand> factory ) where TCommand : Command;

        void Run( string[] args );
    }
}
