using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameBuilder : RtpFrameBuilder
    {
        private readonly object _lock;

        private readonly RtpFrameBuilderConfiguration _configuration;

        private readonly H265FrameFactory _frameFactory;

        private readonly H265FrameAggregator _aggregator;
    




        public H265FrameBuilder()
        {
#if !DEBUG
            throw new NotImplementedException( "the implementation is not finished, this class must not be used in production." );
#endif
            _lock = new object();

            _configuration = new RtpFrameBuilderConfiguration();
            _frameFactory  = new H265FrameFactory();
            _aggregator    = new H265FrameAggregator( this );
        }






        public override FrameBuilderType Type
        {
            get => FrameBuilderType.H265;
        }

        public RtpFrameBuilderConfiguration Configuration
        {
            get => _configuration;
        }


     



        public override void Write( byte[] buffer )
        {
            if ( ! RtpPacket.TryParse( buffer , out RtpPacket packet ) )
            {
                return;
            }

            IEnumerable<RtpFrame> frames = null;

            lock ( _lock )
            {
                if ( !_aggregator.TryAggregate( packet , out IEnumerable<RtpPacket> packets ) )
                {
                    return;
                }

                if ( !_frameFactory.TryCreateFrames( packets , out frames ) )
                {
                    return;
                }
            }

            if ( frames != null )
            {
                foreach ( RtpFrame frame in frames )
                {
                    // Add Frame.Type = I_Frame/P_Frame/B_Frame ?
                    OnFrameReceived( new RtpFrameReceivedEventArgs( frame ) );
                }
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
