using System;
using System.Windows;

namespace RabbitOM.Player.Codecs
{
    public abstract class Renderer : IDisposable
    {
        ~Renderer()
        {
            Dispose( false );
        }

        public abstract bool IsOpened { get; }

        public abstract void Open( FrameworkElement targetControl );

        public abstract void Render( Surface surface );

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