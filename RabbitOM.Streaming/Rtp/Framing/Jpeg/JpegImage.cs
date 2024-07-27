using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public struct JpegImage
    {
        public static readonly JpegImage Empty = new JpegImage();


        private readonly byte[] _data;
        private readonly int _width;
        private readonly int _height;



        public JpegImage( byte[] data , int width , int height )
        {
            if ( data == null )
            {
                throw new ArgumentNullException( nameof( data ) );
            }

            _data = data;
            _width = width;
            _height = height;
        }




        public byte[] Data
        {
            get => _data;
        }

        public int Width
        {
            get => _width;
        }

        public int Height
        {
            get => _height;
        }
    }
}