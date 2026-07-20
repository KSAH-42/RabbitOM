using System;

namespace RabbitOM.Streaming.Rtp.Jpeg
{
    public sealed class JpegMediaElement : RtpMediaElement
    {
        public JpegMediaElement( byte[] buffer , int width , int height ) : base ( buffer )
        {
            Width  = width;
            Height = height;
        }

        public int Width { get; }

        public int Height { get; }
    }
}