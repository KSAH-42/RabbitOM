using System;

namespace RabbitOM.Sample.Player.Codecs
{
    public abstract class Surface : IDisposable
    {
        ~Surface()
        {
            Dispose( false );
        }

        public abstract int FrameWidth { get; }

        public abstract int FrameHeight { get; }

        public abstract IntPtr Frame { get; }

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