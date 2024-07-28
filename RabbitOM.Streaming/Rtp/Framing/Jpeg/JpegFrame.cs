using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent the a jpeg frame
    /// </summary>
    public class JpegFrame : RtpFrame
    {
        private readonly int _width;
        private readonly int _height;

        /// <summary>
        /// Initialize a new instance of the jpeg frame
        /// </summary>
        /// <param name="width">the width</param>
        /// <param name="height">the height</param>
        /// <param name="data">the data</param>
        public JpegFrame( int width , int height , byte[] data )
            : base ( data )
        {
            _width = width;
            _height = height;
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


        /// <summary>
        /// Create an new object
        /// </summary>
        /// <param name="image">the image data structure info</param>
        /// <returns>returns an instance</returns>
        public static JpegFrame NewFrame( JpegImage image )
        {
            return new JpegFrame( image.Width , image.Height , image.Data );
        }
    }
}