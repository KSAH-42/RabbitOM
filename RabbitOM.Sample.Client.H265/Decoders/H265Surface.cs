using System;

namespace RabbitOM.Sample.Client.H265.Codecs
{
    public struct H265Surface
    {
        public H265Surface( int frameWidth , int frameHeight , IntPtr frame )
        {
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            Frame = frame;
        }

        public int FrameWidth { get; }

        public int FrameHeight { get; }

        public IntPtr Frame { get; }
    }
}