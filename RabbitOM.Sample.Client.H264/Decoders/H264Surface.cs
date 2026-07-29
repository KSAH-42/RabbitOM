using System;

namespace RabbitOM.Sample.Client.H264.Codecs
{
    public struct H264Surface
    {
        public H264Surface( int frameWidth , int frameHeight , IntPtr frame )
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