using System;

namespace RabbitOM.Sample.Client.H264.Codecs
{
    public struct H264Surface
    {
        public H264Surface( H264Options options , int frameWidth , int frameHeight , IntPtr decodeFrame )
        {
            Options = options;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            DecodedFrame = decodeFrame;
        }

        public H264Options Options { get; }

        public int FrameWidth { get; }

        public int FrameHeight { get; }

        internal IntPtr DecodedFrame { get; }
    }
}