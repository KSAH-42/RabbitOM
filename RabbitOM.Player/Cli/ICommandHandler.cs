using System;

namespace RabbitOM.Player.Cli
{
    public interface ICommandHandler<in TCommand> where TCommand : Command
    {
        void Execute(TCommand command);
    }
}
