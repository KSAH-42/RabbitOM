/*
 EXPERIMENTATION of the next implementation of the rtp layer
*/

using System;

namespace RabbitOM.Net.Rtp
{
    public class RTPPacketReceivedEventArgs : EventArgs
    {
        public RTPPacketReceivedEventArgs( RTPPacket packet ) => Packet = packet;
        public RTPPacket Packet { get; private set; }
    }
}