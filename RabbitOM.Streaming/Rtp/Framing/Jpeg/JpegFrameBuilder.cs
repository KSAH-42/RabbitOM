using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegFrameBuilder : RtpFrameBuilder
    {
        private readonly JpegFrameBuilderConfiguration _configuration;
        private readonly JpegFrameFactory _factory;
        private readonly JpegFrameAggregator _aggregator;




        public JpegFrameBuilder()
        {
            _configuration = new JpegFrameBuilderConfiguration();
            _factory = new JpegFrameFactory();
            _aggregator = new JpegFrameAggregator( _configuration );
        }




        public JpegFrameBuilderConfiguration Configuration
        {
            get => _configuration;
        }


     

        public override void Write( byte[] buffer )
        {
            if ( ! RtpPacket.TryParse( buffer , out RtpPacket packet ) )
            {
                return;
            }

            if ( _aggregator.TryAggregate( packet , out IEnumerable<RtpPacket> packets ) )
            {
                if ( _factory.TryCreateFrame( packets , out RtpFrame frame ) )
                {
                    OnFrameReceived( new RtpFrameReceivedEventArgs( frame ) );
                }
            }
        }

        public override void Clear()
        {
            _aggregator.Clear();
            _factory.Clear();
        }





        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                _aggregator.Dispose();
                _factory.Dispose();
            }
        }
    }
}
