using System;

namespace RabbitOM.Decoder.FFMpeg.Domain.Media
{
    public sealed class Image
    {
        public Image( byte[] buffer , int width , int height )
        {
            Buffer = buffer ?? throw new ArgumentNullException( nameof( buffer ) );
            Width = width;
            Height = height;
        }

        public byte[] Buffer { get; }

        public int Width { get; }

        public int Height { get; }
    }
}