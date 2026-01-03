using System;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg
{
    /// <summary>
    /// Represent the a jpeg frame
    /// </summary>
    public sealed class JpegFrame : RtpMediaContent
    {
        /// <summary>
        /// Initialize a new instance of the jpeg frame
        /// </summary>
        /// <param name="data">the data</param>
        /// <param name="width">the width</param>
        /// <param name="height">the height</param>
        public JpegFrame( byte[] data , int width , int height ) : base ( data )
        {
            Width  = width;
            Height = height;
        }

        /// <summary>
        /// Gets the width
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the height
        /// </summary>
        public int Height { get; }
    }
}