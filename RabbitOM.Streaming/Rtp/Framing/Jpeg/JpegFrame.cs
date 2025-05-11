using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent the a jpeg frame
    /// </summary>
    public sealed class JpegFrame : RtpFrame
    {
        private readonly int _width;

        private readonly int _height;





        /// <summary>
        /// Initialize a new instance of the jpeg frame
        /// </summary>
        /// <param name="data">the data</param>
        /// <param name="width">the width</param>
        /// <param name="height">the height</param>
        public JpegFrame( byte[] data , int width , int height )
            : base ( data )
        {
            _width  = width;
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
    }
}