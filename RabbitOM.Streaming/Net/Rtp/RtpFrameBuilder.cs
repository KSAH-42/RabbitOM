using System;

namespace RabbitOM.Streaming.Net.Rtp
{
    // TODO: replace "public event EventHandler<TEventArgs> by "public event EventStreamingHeadler" where the EventStreamingHeadler is : public delegate EventStreamingHeadler<T>( object sender, in T evt ); // where T is a struct
    // make if modification if it really brings significant new memory perf otherwize do not change, it is juste a little bit, do not refactor

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





        private readonly RtpPacketAggregator _aggregator = new DefaultRtpPacketAggregator() { MaximumNumberOfPackets = 1000 };
        




        public int MaximumNumberOfPackets
        {
            get => _aggregator.MaximumNumberOfPackets;
            set => _aggregator.MaximumNumberOfPackets = value > 0 ? value : throw new ArgumentOutOfRangeException( nameof( value ) ); 
        }





        public void AddPacket( RtpPacket packet )
        {
            if ( packet == null )
            {
                throw new ArgumentNullException( nameof( packet ) );
            }

            var addingPacket = new RtpPacketAddingEventArgs( packet );
            
            try
            {
                OnPacketAdding( addingPacket );

                if ( ! addingPacket.CanContinue )
                {
                    return;
                }

                _aggregator.AddPacket( packet );

                OnPacketAdded( new RtpPacketAddedEventArgs( packet ) );

                if ( _aggregator.IsSequenceTooLong )
                {
                    _aggregator.RemovePackets();
                    return;
                }

                if ( _aggregator.HasCompleteSequence )
                {
                    if ( _aggregator.HasUnOrderedSequence )
                    {
                        OnSequenceSorting( new RtpSequenceEventArgs( _aggregator.GetSequence() ) );

                        _aggregator.SortSequence();

                        OnSequenceSorted( new RtpSequenceEventArgs( _aggregator.GetSequence() ) );
                    }

                    OnSequenceCompleted( new RtpSequenceEventArgs( _aggregator.GetSequence() ) );

                    _aggregator.RemovePackets();
                }
            }
            catch ( Exception )
            {
                _aggregator.RemovePackets();

                throw;
            }
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

        protected virtual void OnMediaBuilded( RtpMediaBuildedEventArgs e )
        {
            MediaBuilded?.TryInvoke( this , e );
        }

        protected virtual void OnCleared( RtpClearedEventArgs e )
        {
            Cleared?.TryInvoke( this , e );
        }
    }
}
