using System;

namespace RabbitOM.Decoder.FFMpeg.Codecs
{
    public sealed class Image
    {
        public Image( int width , int height , byte[] buffer )
        {
            Width = width;
            Height = height;
            Buffer = buffer ?? throw new ArgumentNullException( nameof( buffer ) );
        }

        public int Width { get; }
        public int Height { get; }
        public byte[] Buffer { get; }
    }
}