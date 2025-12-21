using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    public sealed class H264FrameBuilder : RtpFrameBuilder
    {
        private readonly H264FrameBuilderConfiguration _configuration;

        private readonly H264FrameFactory _frameFactory;

        private readonly H264FrameAggregator _aggregator;
    




        private H264FrameBuilder()
        {
            _configuration = new H264FrameBuilderConfiguration();
            _frameFactory  = new H264FrameFactory( _configuration );
            _aggregator    = new H264FrameAggregator( _configuration );
        }





        public H264FrameBuilderConfiguration Configuration
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

            lock ( SyncRoot )
            {
                if ( ! _aggregator.TryAggregate( packet , out IEnumerable<RtpPacket> packets ) )
                {
                    return;
                }

                if ( ! _frameFactory.TryCreateFrame( packets , out frame ) )
                {
                    return;
                }
            }

            OnFrameReceived( new RtpFrameReceivedEventArgs( frame ) );
        }

        public override void Clear()
        {
            lock ( SyncRoot )
            {
                _aggregator.Clear();
                _frameFactory.Clear();
            }
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                _frameFactory.Dispose();
            }

            base.Dispose( disposing );
        }
    }
}
