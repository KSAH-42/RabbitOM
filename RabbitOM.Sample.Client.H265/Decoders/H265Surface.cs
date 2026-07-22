using System;

namespace RabbitOM.Sample.Client.H265.Codecs
{
    public struct H265Surface
    {
        public H265Surface( H265Options options , int frameWidth , int frameHeight , IntPtr decodeFrame )
        {
            Options = options;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            DecodedFrame = decodeFrame;
        }

        public H265Options Options { get; }

        public int FrameWidth { get; }

        public int FrameHeight { get; }

        internal IntPtr DecodedFrame { get; }
    }
}