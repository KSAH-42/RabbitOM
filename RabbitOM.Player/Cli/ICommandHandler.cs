using System;

namespace RabbitOM.Player.Cli
{
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        void SetHandler( Action<TCommand> handler );
    }
}
