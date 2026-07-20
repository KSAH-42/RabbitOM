using System;

namespace RabbitOM.Sample.Client.H264.Codecs
{
    public class H264DecodedEventArgs : EventArgs
    {
        public H264DecodedEventArgs( H264Surface surface , int frameWidth , int frameHeight )
        {
            Surface = surface;
            FrameWidth = frameWidth;
            FrameHeigth = frameHeight;
        }

        public H264Surface Surface { get ; }

        public int FrameWidth { get; }

        public int FrameHeigth { get; }

        internal H264Context Context { get; }
    }
}