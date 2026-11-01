using System;

namespace RabbitOM.Decoder.FFMpeg.Domain.Codecs
{
    public interface IVideoRendererFactory
    {
        IVideoRenderer CreateRender();
    }
}