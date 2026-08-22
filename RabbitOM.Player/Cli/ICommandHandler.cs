using System;

namespace RabbitOM.Player.Cli
{
    public interface ICommandHandler<TObject>
    {
        void SetHandler( Action<TObject> handler );
    }
}
