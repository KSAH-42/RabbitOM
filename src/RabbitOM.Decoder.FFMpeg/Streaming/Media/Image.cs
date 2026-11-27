// TODO: throw new OutOfRangeArgument/ArgumentException( "ana joh,11" );
using System;

namespace RabbitOM.Decoder.FFMpeg.Streaming.Media
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