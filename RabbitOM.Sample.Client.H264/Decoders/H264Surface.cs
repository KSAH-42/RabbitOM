using System;

namespace RabbitOM.Sample.Client.H264.Codecs
{
    public struct H264Surface
    {
        public H264Surface( int frameWidth , int frameHeight , IntPtr decodeFrame )
        {
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            DecodedFrame = decodeFrame;
        }

        public int FrameWidth { get; }

        public int FrameHeight { get; }

        internal IntPtr DecodedFrame { get; }
    }
}