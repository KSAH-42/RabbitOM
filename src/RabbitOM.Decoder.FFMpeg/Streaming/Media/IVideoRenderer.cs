using System;

namespace RabbitOM.Decoder.FFMpeg.Streaming.Media
{
    public interface IVideoRenderer : IDisposable
    {
        event EventHandler<RenderEventArgs>? Rendered;

        void Render( Surface surface );

        void Close();
    }
}