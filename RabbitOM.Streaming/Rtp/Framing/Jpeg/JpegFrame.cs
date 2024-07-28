using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public class JpegFrame : RtpFrame
    {
        private readonly int _width;
        private readonly int _height;

        public JpegFrame( int width , int height , byte[] data )
            : base ( data )
        {
            _width = width;
            _height = height;
        }

        public int Width
        {
            get => _width;
        }

        public int Height
        {
            get => _height;
        }


        public static JpegFrame NewFrame( JpegImage image )
        {
            return new JpegFrame( image.Width , image.Height , image.Data );
        }
    }
}