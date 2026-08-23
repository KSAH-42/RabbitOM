using System;

namespace RabbitOM.Player.Cli
{
    public abstract class Command : IDisposable
    {
        ~Command()
        {
            Dispose( false );
        }

        public abstract void Execute();

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }
    }
}
