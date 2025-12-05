using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp
{

    /// <summary>
    /// Represent a rtp packet assembler. This class has been introduce to become the central point where packets need to be aggregated.
    /// And it can become an abstract class or it can used a priority queue without modifing all XXXFrameAggregator class.
    /// This class has been introduce to avoid coupling based on inheritance by using a BaseFrameAggregator class, this is the main reason.
    /// </summary>
    public sealed class RtpPacketAggregator
    {
        private readonly RtpPacketQueue _queue = new RtpPacketQueue();

        private uint? _currentSequenceNumber;

        private bool _isUnOrdered;




        /// <summary>
        /// Gets the packets
        /// </summary>
        public IReadOnlyCollection<RtpPacket> Packets
        {
            get => _queue;
        }

        /// <summary>
        /// Gets the current sequence number
        /// </summary>
        public uint? CurrentSequenceNumber
        {
            get => _currentSequenceNumber;
        }

        /// <summary>
        /// Gets the status if the sequence is ordered or not
        /// </summary>
        public bool IsUnOrdered
        {
            get => _isUnOrdered;
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
        public bool TryAggregate( RtpPacket packet , out IEnumerable<RtpPacket> packets )
        {
            packets = default;

            if ( _queue.TryEnqueue( packet ) )
            {
                OnPacketAdded( packet );

                if ( packet.Marker )
                {
                    packets = _isUnOrdered ? RtpPacketQueue.Sort( _queue ) : _queue.AsEnumerable();

                    _queue.Clear();

                    _isUnOrdered = false;

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _queue.Clear();

            _currentSequenceNumber = null;
            _isUnOrdered = false;
        }




        /// <summary>
        /// Occurs when a packet is added
        /// </summary>
        /// <param name="packet">the packet</param>
        private void OnPacketAdded( RtpPacket packet )
        {
            if ( _currentSequenceNumber.HasValue )
            {
                var diff = packet.SequenceNumber - _currentSequenceNumber;

                _isUnOrdered |= diff != 1 && packet.SequenceNumber > 1;
            }

            _currentSequenceNumber = packet.SequenceNumber;
        }
    }
}