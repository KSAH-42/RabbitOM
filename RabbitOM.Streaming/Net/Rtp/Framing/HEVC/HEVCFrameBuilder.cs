using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.Framing.HEVC
{
    public sealed class HEVCFrameBuilder : RtpFrameBuilder
    {
        private readonly HEVCFrameBuilderConfiguration _configuration;

        private readonly HEVCFrameFactory _frameFactory;

        private readonly HEVCFrameAggregator _aggregator;
    




        public HEVCFrameBuilder()
        {
            _configuration = new HEVCFrameBuilderConfiguration();
            _frameFactory  = new HEVCFrameFactory( _configuration );
            _aggregator    = new HEVCFrameAggregator( _configuration );
        }





        public HEVCFrameBuilderConfiguration Configuration
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
            if ( ! disposing )
            {
                return;
            }

            lock ( SyncRoot )
            {
                _frameFactory.Dispose();
            }
        }
    }
}
