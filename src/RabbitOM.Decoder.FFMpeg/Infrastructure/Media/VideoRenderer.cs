using System;

namespace RabbitOM.Decoder.FFMpeg.Infrastructure.Media
{
    using RabbitOM.Decoder.FFMpeg.Domain.Media;

    public abstract class VideoRenderer : IVideoRenderer
    {
        public event EventHandler<RenderEventArgs>? Rendered;

        ~VideoRenderer()
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

        protected virtual void OnRender( RenderEventArgs e )
        {
            Rendered?.Invoke( this, e );
        }
    }
}