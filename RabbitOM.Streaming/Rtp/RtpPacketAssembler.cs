using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp
{
    public sealed class RtpPacketAssembler
    {
        private readonly RtpPacketQueue _packets = new RtpPacketQueue();
 




        public IReadOnlyCollection<RtpPacket> Packets
        {
            get => _packets;
        }





        public bool TryAssemble( RtpPacket packet , out IEnumerable<RtpPacket> result )
        {
            result = null;

            if ( packet == null )
            {
                return false;
            }

            _packets.Enqueue( packet );

            if ( packet.Marker )
            {
                result = RtpPacketQueue.CanSort( _packets ) ? RtpPacketQueue.Sort( _packets ) : _packets.AsEnumerable();

                _packets.Clear();
            }

            return result != null;
        }

        public void Clear()
        {
            _packets.Clear();
        }
    }
}