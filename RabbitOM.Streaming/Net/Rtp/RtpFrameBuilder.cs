using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RabbitOM.Streaming.Net.Rtp
{
    public abstract class RtpFrameBuilder : IFrameBuilder , IDisposable
    {
        public const int DefaultMTU = 1500;

        public const int DefaultMaximumOfPackets = 1000;

        public const int DefaultMaximumOfPacketsSize = DefaultMTU * 4;







        public event EventHandler<RtpPacketAddedEventArgs> PacketAdded;

        public event EventHandler<RtpPacketAddingEventArgs> PacketAdding;

        public event EventHandler<RtpSequenceCompletedEventArgs> SequenceCompleted;

        public event EventHandler<RtpSequenceSortingEventArgs> SequenceSorting;

        public event EventHandler<RtpFrameReceivedEventArgs> FrameReceived;

        public event EventHandler<RtpClearedEventArgs> Cleared;







        private readonly RtpPacketAggregator _aggregator = new DefaultRtpPacketAggregator();
        
        private int _maximumOfPackets = DefaultMaximumOfPackets;

        private int _maximumOfPacketsSize = DefaultMaximumOfPacketsSize;







        ~RtpFrameBuilder()
        {
            Dispose( false );
        }
        






        public IReadOnlyCollection<RtpPacket> Packets
        {
            get => _aggregator.Packets;
        }

        public int MaximumOfPackets
        {
            get => _maximumOfPackets;
        }

        public int MaximumOfPacketsSize
        {
            get => _maximumOfPacketsSize;
        }
        






        public void Configure( int maximumOfPackets , int maximumOfPacketsSize )
        {
            _maximumOfPackets = maximumOfPackets > 0 ? maximumOfPackets : throw new ArgumentOutOfRangeException( nameof( maximumOfPackets ) );
            _maximumOfPacketsSize = maximumOfPacketsSize > 0 ? maximumOfPacketsSize : throw new ArgumentOutOfRangeException( nameof( maximumOfPacketsSize ) );
        }

        public void AddPacket( byte[] buffer )
        {
            if ( RtpPacket.TryParse( buffer , out var packet ) )
            {
                AddPacket( packet );
            }
        }

        public void AddPacket( RtpPacket packet )
        {
            if ( packet == null || ! packet.TryValidate() )
            {
                return;
            }

            if ( packet.Payload.Count >= MaximumOfPacketsSize || _aggregator.Packets.Count >= MaximumOfPackets )
            {
                return;
            }

            var addingPacket = new RtpPacketAddingEventArgs(packet );

            OnPacketAdding( addingPacket );

            if ( ! addingPacket.Continue )
            {
                return;
            }

            _aggregator.AddPacket( packet );

            OnPacketAdded( new RtpPacketAddedEventArgs( packet ) );

            if ( ! _aggregator.HasCompleteSequence )
            {
                return;
            }

            if ( _aggregator.HasUnOrderedSequence )
            {
                OnSequenceSorting( new RtpSequenceSortingEventArgs( _aggregator.GetSequence() ) );

                _aggregator.SortSequence();
            }
                
            OnSequenceCompleted( new RtpSequenceCompletedEventArgs( _aggregator.GetSequence() ) );

            _aggregator.RemovePackets();
        }

        public void RemovePackets()
        {
            _aggregator.RemovePackets();
        }

        public void Clear()
        {
            _aggregator.Clear();

            OnCleared( new RtpClearedEventArgs() );
        }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( disposing )
            {
                Clear();
            }
        }
        









        protected virtual void OnPacketAdded( RtpPacketAddedEventArgs e )
        {
            PacketAdded?.TryInvoke( this , e );
        }

        protected virtual void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            PacketAdding?.TryInvoke( this , e );
        }

        protected virtual void OnSequenceCompleted( RtpSequenceCompletedEventArgs e )
        {
            SequenceCompleted?.TryInvoke( this , e );
        }

        protected virtual void OnSequenceSorting( RtpSequenceSortingEventArgs e )
        {
            SequenceSorting?.TryInvoke( this , e );
        }

        protected virtual void OnFrameReceived( RtpFrameReceivedEventArgs e )
        {
            FrameReceived?.TryInvoke( this , e );
        }

        protected virtual void OnCleared( RtpClearedEventArgs e )
        {
            Cleared?.TryInvoke( this , e );
        }
    }
}
