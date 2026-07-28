using System;
using System.Windows;

namespace RabbitOM.Sample.Client.H264.Codecs
{
    public abstract class H264Renderer : IDisposable
    {
        ~H264Renderer()
        {
            Dispose( false );
        }

        public abstract bool IsOpened { get; }

        public abstract void Open( FrameworkElement targetControl );

        public abstract void Render( H264Surface surface );

        public abstract void Close();

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( disposing )
            {
                Close();
            }
        }
    }
}