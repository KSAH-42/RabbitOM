using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent a simple data structure
    /// </summary>
    public struct JpegImage
    {
        /// <summary>
        /// Empty value
        /// </summary>
        public static readonly JpegImage Empty = new JpegImage();



        
        private readonly byte[] _data;
        
        private readonly int _width;
        
        private readonly int _height;





        /// <summary>
        /// Initialize an instance
        /// </summary>
        /// <param name="data">the data</param>
        /// <param name="width">the width</param>
        /// <param name="height">the height</param>
        /// <exception cref="ArgumentNullException"/>
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





        /// <summary>
        /// Gets the image in binary form
        /// </summary>
        public byte[] Data
        {
            get => _data;
        }

        /// <summary>
        /// Gets the width
        /// </summary>
        public int Width
        {
            get => _width;
        }

        /// <summary>
        /// Gets the height
        /// </summary>
        public int Height
        {
            get => _height;
        }
    }
}