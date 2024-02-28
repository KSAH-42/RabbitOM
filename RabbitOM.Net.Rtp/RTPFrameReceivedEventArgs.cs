/*
 EXPERIMENTATION of the next implementation of the rtp layer

              O P T I M I Z A T I O N S

 For next implementation, optimize can to reduce copy 
 by using array segment to remove Buffer.Copy or similar methods

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