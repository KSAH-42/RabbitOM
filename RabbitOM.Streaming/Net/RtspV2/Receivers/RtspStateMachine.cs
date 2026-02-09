using System;

namespace RabbitOM.Streaming.Net.RtspV2.Receivers
{
    public abstract class RtspStateMachine : IDisposable
    {
        public abstract TimeSpan IdleTime { get; }




        public abstract void Run();




        ~RtspStateMachine()
        {
            Dispose( false );
        }

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
