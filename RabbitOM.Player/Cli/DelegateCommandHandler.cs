using System;

namespace RabbitOM.Player.Cli
{
    public sealed class DelegateCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : Command
    {
        private readonly Action<TCommand> _handler;

        public DelegateCommandHandler( Action<TCommand> handler )
        {
            _handler = handler ?? throw new ArgumentNullException( nameof( handler ) );
        }

        public void Execute(TCommand command)
        {
            _handler( command );
        }
    }
}
