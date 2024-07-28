using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    public sealed class JpegFrameBuilder : RtpFrameBuilder
    {
        private readonly object _lock;
        private readonly JpegFrameBuilderConfiguration _configuration;
        private readonly JpegFrameFactory _factory;
        private readonly JpegFrameAggregator _aggregator;




        public JpegFrameBuilder()
        {
            _lock = new object();
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

            RtpFrame frame = null;

            lock ( _lock )
            {
                if ( ! _aggregator.TryAggregate( packet , out IEnumerable<RtpPacket> packets ) )
                {
                    return;
                }

                if ( ! _factory.TryCreateFrame( packets , out frame ) )
                {
                    return;
                }
            }

            OnFrameReceived( new RtpFrameReceivedEventArgs( frame ) );

        }

        public override void Clear()
        {
            lock ( _lock )
            {
                _aggregator.Clear();
                _factory.Clear();
            }
        }





        protected override void Dispose( bool disposing )
        {
            lock ( _lock )
            {
                _aggregator.Dispose();
                _factory.Dispose();
            }
        }
    }
}
