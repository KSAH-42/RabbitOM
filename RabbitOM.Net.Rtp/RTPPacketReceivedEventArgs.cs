﻿/*
 EXPERIMENTATION of the next implementation of the rtp layer

              O P T I M I Z A T I O N S

 For next implementation, optimize can to reduce copy 
 by using array segment to remove Buffer.Copy or similar methods

*/

using System;

namespace RabbitOM.Net.Rtsp.Tests
{
    public class RTPPacketReceivedEventArgs : EventArgs
    {
        public RTPPacketReceivedEventArgs( RTPPacket packet ) => Packet = packet;
        public RTPPacket Packet { get; private set; }
    }
}