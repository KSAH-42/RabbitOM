using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
    /// <summary>
    /// Represent the frame builder class
    /// </summary>
    public sealed class JpegFrameBuilder : RtpFrameBuilder
    {
        private readonly object _lock;

        private readonly JpegFrameBuilderConfiguration _configuration;
        
        private readonly JpegFrameFactory _factory;
        
        private readonly JpegFrameAggregator _aggregator;





        /// <summary>
        /// Initialize a new instance of the frame builder class
        /// </summary>
        public JpegFrameBuilder()
        {
            _lock          = new object();

            _configuration = new JpegFrameBuilderConfiguration();
            _factory       = new JpegFrameFactory();
            _aggregator    = new JpegFrameAggregator( this );
        }





        
        /// <summary>
        /// Gets the builder configuration settings
        /// </summary>
        public RtpFrameBuilderConfiguration Configuration
        {
            get => _configuration;
        }


     



        /// <summary>
        /// Write the rtp packet
        /// </summary>
        /// <param name="buffer">the buffer</param>
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

        /// <summary>
        /// Clear
        /// </summary>
        public override void Clear()
        {
            lock ( _lock )
            {
                _aggregator.Clear();
                _factory.Clear();
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">true for disposing...</param>
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
