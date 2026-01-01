using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    /// <summary>
    /// Represent a H264 frame builder
    /// </summary>
    public sealed class H264FrameBuilder : RtpFrameBuilder
    {
        private readonly H264FrameBuilderConfiguration _configuration;

        private readonly H264FrameFactory _frameFactory;

        private readonly H264FrameAggregator _aggregator;
    




        /// <summary>
        /// Intialize a new instance of the H264 frame builder
        /// </summary>
        public H264FrameBuilder()
        {
            _configuration = new H264FrameBuilderConfiguration();
            _frameFactory  = new H264FrameFactory( _configuration );
            _aggregator    = new H264FrameAggregator( _configuration );
        }






        /// <summary>
        /// Gets the configuration
        /// </summary>
        public H264FrameBuilderConfiguration Configuration
        {
            get => _configuration;
        }
        





        /// <summary>
        /// Setup
        /// </summary>
        public override void Setup()
        {
            lock ( SyncRoot )
            {
                _frameFactory.Setup();
            }
        }

        /// <summary>
        /// Write the buffer
        /// </summary>
        /// <param name="buffer">the buffer</param>
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

        /// <summary>
        /// Clear
        /// </summary>
        public override void Clear()
        {
            lock ( SyncRoot )
            {
                _aggregator.Clear();
                _frameFactory.Clear();
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
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
