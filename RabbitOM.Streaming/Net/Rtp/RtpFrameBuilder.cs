using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    public abstract class RtpFrameBuilder : IMediaBuilder , IDisposable
    {
        public event EventHandler<RtpPacketAddingEventArgs> PacketAdding;

        public event EventHandler<RtpPacketAddedEventArgs> PacketAdded;

        public event EventHandler<RtpSequenceEventArgs> SequenceSorting;

        public event EventHandler<RtpSequenceEventArgs> SequenceSorted;

        public event EventHandler<RtpSequenceEventArgs> SequenceCompleted;

        public event EventHandler<RtpMediaBuildedEventArgs> MediaBuilded;

        public event EventHandler<RtpClearedEventArgs> Cleared;

        






        ~RtpFrameBuilder()
        {
            Dispose( false );
        }






        private readonly RtpPacketAggregator _aggregator = new DefaultRtpPacketAggregator();
        
       




        public void AddPacket( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
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
                OnSequenceSorting( new RtpSequenceEventArgs( _aggregator.GetSequence() ) );

                _aggregator.SortSequence();

                OnSequenceSorted( new RtpSequenceEventArgs( _aggregator.GetSequence() ) );
            }
                
            OnSequenceCompleted( new RtpSequenceEventArgs( _aggregator.GetSequence() ) );

            _aggregator.RemovePackets();
        }

        public void Clear()
        {
            _aggregator.Clear();

            OnCleared( RtpClearedEventArgs.Default );
        }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }
        







        protected virtual void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            PacketAdding?.TryInvoke( this , e );
        }

        protected virtual void OnPacketAdded( RtpPacketAddedEventArgs e )
        {
            PacketAdded?.TryInvoke( this , e );
        }

        protected virtual void OnSequenceSorting( RtpSequenceEventArgs e )
        {
            SequenceSorting?.TryInvoke( this , e );
        }

        protected virtual void OnSequenceSorted( RtpSequenceEventArgs e )
        {
            SequenceSorted?.TryInvoke( this , e );
        }

        protected virtual void OnSequenceCompleted( RtpSequenceEventArgs e )
        {
            SequenceCompleted?.TryInvoke( this , e );
        }

        protected virtual void OnBuilded( RtpMediaBuildedEventArgs e )
        {
            MediaBuilded?.TryInvoke( this , e );
        }

        protected virtual void OnCleared( RtpClearedEventArgs e )
        {
            Cleared?.TryInvoke( this , e );
        }
    }
}
