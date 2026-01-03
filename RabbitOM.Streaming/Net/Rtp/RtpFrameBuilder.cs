// TODO: adding errors count, stats ??? 
// TODO: on the add method, need to discard the actual sequence, in case of receiving one invalid or missing packet ?

using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp
{
    public abstract class RtpFrameBuilder : IRtpMediaBuilder , IDisposable
    {
        public event EventHandler<RtpPacketAddingEventArgs> PacketAdding;

        public event EventHandler<RtpPacketAddedEventArgs> PacketAdded;

        public event EventHandler<RtpBuildEventArgs> Builded;

        public event EventHandler Cleared;

        public event EventHandler<RtpSequenceCompletedEventArgs> SequenceCompleted;

        public event EventHandler<RtpSequenceSortingEventArgs> SequenceSorting;


        ~RtpFrameBuilder()
        {
            Dispose( false );
        }




        private readonly RtpPacketAggregator _aggregator = new DefaultRtpPacketAggregator();
        
        private int _maximumOfPackets = RtpConstants.DefaultMaximumOfPackets;

        private int _maximumOfPacketsSize = RtpConstants.DefaultMaximumOfPacketsSize;






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

            if ( ! addingPacket.CanContinue )
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

        public void Clear()
        {
            _aggregator.Clear();

            OnCleared( EventArgs.Empty );
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
        







        protected virtual void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            PacketAdding?.TryInvoke( this , e );
        }

        protected virtual void OnPacketAdded( RtpPacketAddedEventArgs e )
        {
            PacketAdded?.TryInvoke( this , e );
        }

        protected virtual void OnBuild( RtpBuildEventArgs e )
        {
            Builded?.TryInvoke( this , e );
        }

        protected virtual void OnCleared( EventArgs e )
        {
            Cleared?.TryInvoke( this , e );
        }

        protected virtual void OnSequenceSorting( RtpSequenceSortingEventArgs e )
        {
            SequenceSorting?.TryInvoke( this , e );
        }

        protected virtual void OnSequenceCompleted( RtpSequenceCompletedEventArgs e )
        {
            SequenceCompleted?.TryInvoke( this , e );
        }
    }
}
