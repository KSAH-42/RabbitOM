using System;

namespace RabbitOM.Sample.Client.H265.Codecs
{
    public abstract class H265Renderer : IDisposable
    {
        ~H265Renderer()
        {
            Dispose( false );
        }

        public abstract void Render( H265Surface surface );

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