using System;
using System.Windows.Media.Imaging;

namespace RabbitOM.Sample.Client.H264.Codecs
{
    public abstract class H264Renderer : IDisposable
    {
        ~H264Renderer()
        {
            Dispose( false );
        }

        public abstract void Render( H264Surface surface );

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