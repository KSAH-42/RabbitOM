using System;

namespace RabbitOM.Decoder.FFMpeg.Infrastructure.Codecs
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