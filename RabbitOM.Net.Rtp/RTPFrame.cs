/*
 EXPERIMENTATION of the next implementation of the rtp layer

              O P T I M I Z A T I O N S

 For next implementation, optimize can to reduce copy 
 by using array segment to remove Buffer.Copy or similar methods

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