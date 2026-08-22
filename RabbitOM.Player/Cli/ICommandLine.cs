using System;

namespace RabbitOM.Player.Cli
{
    public interface ICommandLine : IDisposable
    {
        ICommandLine Register<TCommand>( Action<TCommand> action ) where TCommand : Command;

        void Run( string[] args );
    }
}
