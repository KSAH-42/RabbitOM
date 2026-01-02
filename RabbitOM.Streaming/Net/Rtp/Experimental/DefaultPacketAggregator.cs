using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Net.Rtp.Experimental
{
    public sealed class DefaultPacketAggregator : PacketAggregator
    {
        private readonly Queue<RtpPacket> _packets = new Queue<RtpPacket>();

        private bool _isCompleted;

        private bool _isUnOrdered;

        private uint? _currentSequenceNumber;

        private IReadOnlyCollection<RtpPacket> _sequence;



        public override IReadOnlyCollection<RtpPacket> Packets
        {
            get => _packets;
        }

        public override bool HasCompleteSequence
        {
            get => _isCompleted;
        }

        public override bool HasUnOrderedSequence
        {
            get => _isUnOrdered;
        }



        public override void AddPacket( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            if ( _isCompleted )
            {
                throw new InvalidOperationException();
            }

            _packets.Enqueue( packet );

            OnPacketAdded( packet );

            if ( packet.Marker )
            {
                _isCompleted = true;
            }
        }

        public override void RemovePackets()
        {
            _packets.Clear();

            _isCompleted = false;
            _isUnOrdered = false;
            _sequence = null;
        }

        public override void Clear()
        {
            RemovePackets();

            _currentSequenceNumber = null;
        }

        public override void SortSequence()
        {
            _sequence = _packets.OrderBy( packet => packet.SequenceNumber ).ToList();

            _isUnOrdered = false;
        }

        public override IReadOnlyCollection<RtpPacket> GetSequence()
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
