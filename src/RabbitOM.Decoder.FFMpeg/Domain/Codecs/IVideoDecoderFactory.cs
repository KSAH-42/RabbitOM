using System;

namespace RabbitOM.Decoder.FFMpeg.Domain.Codecs
{
    public interface IVideoDecoderFactory
    {
        bool CanCreateDecoder( string value );

        IVideoDecoder CreateDecoder( string value );
    }
}