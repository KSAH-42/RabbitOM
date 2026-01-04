using System;

namespace RabbitOM.Streaming.Net.Rtp.Jpeg
{
    /// <summary>
    /// Represent the a jpeg frame
    /// </summary>
    public sealed class JpegFrameMediaElement : RtpMediaElement
    {
        /// <summary>
        /// Initialize a new instance of the jpeg frame
        /// </summary>
        /// <param name="width">the width</param>
        /// <param name="height">the height</param>
        /// <param name="buffer">the buffer</param>
        public JpegFrameMediaElement( int width , int height , byte[] buffer ) : base ( buffer )
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