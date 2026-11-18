using RabbitOM.Decoder.FFMpeg.Streaming.Media;
using System;

namespace RabbitOM.Decoder.FFMpeg.Streaming.Media.Renders
{
    public interface IVideoRenderer : IDisposable
    {
        event EventHandler<RenderEventArgs>? Rendered;

        void Render( Surface surface );

        void Close();
    }
}