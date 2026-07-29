using System;

namespace RabbitOM.Sample.Client.H264.Codecs
{
    public abstract class H264Surface : IDisposable
    {
        ~H264Surface()
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