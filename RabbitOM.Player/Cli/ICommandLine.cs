using System;

namespace RabbitOM.Player.Cli
{
    public interface ICommandLine : IDisposable
    {
        ICommandLine AddCommand<TCommand>( Func<TCommand> action ) where TCommand : Command;

        void Run( string[] args );
    }
}
