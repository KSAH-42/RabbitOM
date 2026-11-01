using System;

namespace RabbitOM.Decoder.FFMpeg.Infra.Codecs
{
    using RabbitOM.Decoder.FFMpeg.Domain.Codecs;

    public sealed class VideoRendererFactory : IVideoRendererFactory
    {
        public IVideoRenderer CreateRender()
        {
            return new SoftwareVideoRenderer();
        }
    }
}