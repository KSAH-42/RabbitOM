using System;

namespace RabbitOM.Net.RtspV2.Receivers
{
    public abstract class RtspStateMachine : IDisposable
    {
        public abstract TimeSpan IdleTime { get; }

        public abstract void Run();

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
