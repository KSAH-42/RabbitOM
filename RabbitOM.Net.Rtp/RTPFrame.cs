/*
 EXPERIMENTATION of the next implementation of the rtp layer
*/

using System;

namespace RabbitOM.Net.Rtp
{
    public sealed class RTPFrame
    {
        public RTPFrame( RTPPacket[] packets ) 
            => Packets = packets;
        public RTPPacket[] Packets { get; private set; }

        public bool TryValidate()
            => Packets == null || Packets.Length == 0 ? false : true
            ;
    }
}