using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameBuilder : RtpFrameBuilder
    {
        private readonly object _lock;

        private readonly H265FrameBuilderConfiguration _configuration;

        private readonly H265FrameFactory _frameFactory;

        private readonly H265FrameAggregator _aggregator;
    




        public H265FrameBuilder()
        {
#if !DEBUG
            throw new NotImplementedException( "the implementation is not yet finished" );
#endif
            _lock = new object();

            _configuration = new H265FrameBuilderConfiguration();
            _frameFactory  = new H265FrameFactory();
            _aggregator    = new H265FrameAggregator( this );
        }






        public override FrameBuilderType Type
        {
            get => FrameBuilderType.H265;
        }

        public H265FrameBuilderConfiguration Configuration
        {
            get => _configuration;
        }


     



        public override void Write( byte[] buffer )
        {
            if ( ! RtpPacket.TryParse( buffer , out RtpPacket packet ) )
            {
                return;
            }

            RtpFrame frame = null;

            lock ( _lock )
            {
                if ( !_aggregator.TryAggregate( packet , out IEnumerable<RtpPacket> packets ) )
                {
                    return;
                }

                if ( !_frameFactory.TryCreateFrame( packets , out frame ) )
                {
                    return;
                }
            }

            if ( frame != null )
            {
                OnFrameReceived( new RtpFrameReceivedEventArgs( frame ) );
            }
        }

        public override void Clear()
        {
            lock ( _lock )
            {
                _aggregator.Clear();
                _frameFactory.Clear();
            }
        }

        protected override void Dispose( bool disposing )
        {
            if ( ! disposing )
            {
                return;
            }

            lock ( _lock )
            {
                _aggregator.Dispose();
                _frameFactory.Dispose();
            }
        }
    }
}
