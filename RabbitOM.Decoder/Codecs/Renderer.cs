using System;

namespace RabbitOM.Decoder.Codecs
{
    public abstract class Renderer : IDisposable
    {
        ~Renderer()
        {
            Dispose( false );
        }

        public abstract void Render( Surface surface );

        public abstract void Close();

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            Close();
        }
    }
}