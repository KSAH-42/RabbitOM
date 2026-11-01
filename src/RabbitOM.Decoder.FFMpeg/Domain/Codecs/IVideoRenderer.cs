using System;

namespace RabbitOM.Decoder.FFMpeg.Domain.Codecs
{
    public interface IVideoRenderer : IDisposable
    {
        event EventHandler<RenderEventArgs>? Rendered;

        void Render( Surface surface );

        void Close();
    }
}