using System;

namespace RabbitOM.Player.Cli
{
    public abstract class Command
    {
        public virtual bool TryValidate()
        {
            return true;
        }
    }
}
