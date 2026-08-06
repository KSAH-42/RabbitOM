using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Rtp
{
    public sealed class RtpPacketAggregator : IPacketAggregator
    {
        private readonly Queue<RtpPacket> _packets = new Queue<RtpPacket>();

        private bool _isCompleted;

        private bool _isUnOrdered;

        private uint? _currentSequenceNumber;

        private int _maximumNumberOfPackets;

        private IReadOnlyCollection<RtpPacket> _sequence;






        public int MaximumNumberOfPackets
        {
            get => _maximumNumberOfPackets;
            set => _maximumNumberOfPackets = value;
        }

        public bool IsSequenceTooLong
        {
            get => _maximumNumberOfPackets <= _packets.Count;
        }

        public bool HasCompleteSequence
        {
            get => _isCompleted;
        }

        public bool HasUnOrderedSequence
        {
            get => _isUnOrdered;
        }

        public IReadOnlyCollection<RtpPacket> Packets
        {
            get => _packets;
        }

        






        public void AddPacket( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( _isCompleted || _maximumNumberOfPackets <= _packets.Count )
            {
                throw new InvalidOperationException( "The aggregator contains remaining packets and full sequence, adding one more packet will break the sequence. Please, retrieve the sequence if you need it and remove all remaining packets." );
            }

            _packets.Enqueue( packet );

            OnPacketAdded( packet );

            if ( packet.Marker )
            {
                _isCompleted = true;
            }
        }

        public void RemovePackets()
        {
            _packets.Clear();

            _isCompleted = false;
            _isUnOrdered = false;
            _sequence = null;
        }

        public void Clear()
        {
            RemovePackets();

            _currentSequenceNumber = null;
        }

        public void SortSequence()
        {
            _sequence = _packets.OrderBy( packet => packet.SequenceNumber ).ToList();

            _isUnOrdered = false;
        }

        public IReadOnlyCollection<RtpPacket> GetSequence()
        {
            return _sequence ?? _packets;
        }







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
