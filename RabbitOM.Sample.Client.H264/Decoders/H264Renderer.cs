using System;

namespace RabbitOM.Sample.Client.H264.Codecs
{
    public abstract class H264Renderer : IDisposable
    {
        ~H264Renderer()
        {
            Dispose( false );
        }

        public abstract void Render( H264Surface surface );

        public abstract void Invalidate();

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