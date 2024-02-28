/*
 EXPERIMENTATION of the next implementation of the rtp layer
*/

using System;

namespace RabbitOM.Net.Rtp
{
    public class RTPFrameReceivedEventArgs : EventArgs
    {
        public RTPFrameReceivedEventArgs( RTPFrame frame ) => Frame = frame;
        public RTPFrame Frame { get; private set; }
    }
}