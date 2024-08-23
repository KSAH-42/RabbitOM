﻿using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp
{
    /// <summary>
    /// Represent a rtp packet assembler. This class has been introduce to become the central point where packet need to aggregated.
    /// And it can become an abstract class or it can used a priority queue without modifing all XXXFrameAggregator class.
    /// This class has been introduce to avoid coupling based inheritance by using a BaseFrameAggregator class used by the different aggregator.
    /// </summary>
    public sealed class RtpPacketAssembler
    {
        private readonly RtpPacketQueue _packets = new RtpPacketQueue();
 

        /// <summary>
        /// Gets the packets as a readonly collection
        /// </summary>
        public IReadOnlyCollection<RtpPacket> Packets
        {
            get => _packets;
        }



        /// <summary>
        /// Try to assemble packet in one single unit
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <param name="result">the list of aggregated packed if it succeed</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <remarks>
        ///     <para>If the method succeed, all pending <see cref="Packets"/> will be removed.</para>
        /// </remarks>
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

        /// <summary>
        /// Remove all packets
        /// </summary>
        public void Clear()
        {
            _packets.Clear();
        }
    }
}