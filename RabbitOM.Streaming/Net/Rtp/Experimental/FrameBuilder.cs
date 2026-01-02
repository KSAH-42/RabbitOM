using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Experimental
{
    public abstract class FrameBuilder : IFrameBuilder , IDisposable
    {
        public const int DefaultMaximumOfPackets = 1000;

        public const int DefaultMaximumOfPacketsSize = 1500 * 3;




        public event EventHandler<PacketAddingEventArgs> PacketAdding;

        public event EventHandler<PacketAddedEventArgs> PacketAdded;

        public event EventHandler<SortingSequenceEventArgs> SortingSequence;

        public event EventHandler<SequenceCompletedEventArgs> SequenceCompleted;

        public event EventHandler<FrameReceivedEventArgs> FrameReceived;

        public event EventHandler<ClearedEventArgs> Cleared;







        private readonly PacketAggregator _aggregator = new DefaultPacketAggregator();
        






        ~FrameBuilder()
        {
            Dispose( false );
        }
        






        public IReadOnlyCollection<RtpPacket> Packets { get => _aggregator.Packets; }

        public int MaximumOfPackets { get; } = DefaultMaximumOfPackets;

        public int MaximumOfPacketsSize { get; } = DefaultMaximumOfPacketsSize;
        






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

            var addingPacket = new PacketAddingEventArgs(packet );

            OnPacketAdding( addingPacket );

            if ( ! addingPacket.Continue )
            {
                return;
            }

            _aggregator.AddPacket( packet );

            OnPacketAdded( new PacketAddedEventArgs(packet) );

            if ( _aggregator.HasCompleteSequence )
            {
                if ( _aggregator.HasUnOrderedSequence )
                {
                    OnSortingSequence( new SortingSequenceEventArgs( _aggregator.GetSequence() ) );

                    _aggregator.SortSequence();
                }
                
                OnSequenceCompleted( new SequenceCompletedEventArgs( _aggregator.GetSequence() ) );

                _aggregator.RemovePackets();
            }
        }

        public void RemovePackets()
        {
            _aggregator.RemovePackets();
        }

        public void Clear()
        {
            _aggregator.Clear();

            OnCleared( new ClearedEventArgs() );
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
        






        protected virtual void OnPacketAdding( PacketAddingEventArgs e )
        {
            PacketAdding?.Invoke( this , e );
        }

        protected virtual void OnPacketAdded( PacketAddedEventArgs e )
        {
            PacketAdded?.Invoke( this , e );
        }

        protected virtual void OnSortingSequence( SortingSequenceEventArgs e )
        {
            SortingSequence?.Invoke( this , e );
        }

        protected virtual void OnSequenceCompleted( SequenceCompletedEventArgs e )
        {
            SequenceCompleted?.Invoke( this , e );
        }

        protected virtual void OnFrameReceived( FrameReceivedEventArgs e )
        {
            FrameReceived?.Invoke( this , e );
        }

        protected virtual void OnCleared( ClearedEventArgs e )
        {
            Cleared?.Invoke( this , e );
        }
    }
}
