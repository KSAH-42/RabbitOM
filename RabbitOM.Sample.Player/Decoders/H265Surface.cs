using System;

namespace RabbitOM.Sample.Client.Player.Codecs
{
    public abstract class H265Surface : IDisposable
    {
        ~H265Surface()
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