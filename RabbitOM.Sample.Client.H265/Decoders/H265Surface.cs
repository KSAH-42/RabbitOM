using System;

namespace RabbitOM.Sample.Client.H265.Codecs
{
    public struct H265Surface
    {
        public H265Surface( int frameWidth , int frameHeight , IntPtr decodeFrame )
        {
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            DecodedFrame = decodeFrame;
        }

        public int FrameWidth { get; }

        public int FrameHeight { get; }

        public IntPtr DecodedFrame { get; }
    }
}