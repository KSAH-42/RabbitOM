// we remove the command.execute method in order to allow execute the same command with different code
// for instance is to avoid to recreate the same command class but we need to execute a different task
// so command handler will used here
using System;

namespace RabbitOM.Player.Cli
{
    public interface ICommandHandler<in TCommand> where TCommand : Command
    {
        void Execute(TCommand command);
    }
}
