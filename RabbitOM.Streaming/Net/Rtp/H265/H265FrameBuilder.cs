using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    /// <summary>
    /// Represent a H265 frame builder
    /// </summary>
    public sealed class H265FrameBuilder : RtpFrameBuilder
    {
        private readonly H265FrameBuilderConfiguration _configuration;

        private readonly H265FrameAggregator _aggregator;

        private readonly H265FrameFactory _frameFactory;
    




        /// <summary>
        /// Initialize a new instance of a H265 frame builder
        /// </summary>
        public H265FrameBuilder()
        {
            _configuration = new H265FrameBuilderConfiguration();
            _aggregator    = new H265FrameAggregator( _configuration );
            _frameFactory  = new H265FrameFactory( _configuration );
        }





        /// <summary>
        /// Gets the configuration used by the builder
        /// </summary>
        public H265FrameBuilderConfiguration Configuration
        {
            get => _configuration;
        }





        /// <summary>
        /// Setup the builder
        /// </summary>
        public override void Setup()
        {
            lock ( SyncRoot )
            {
                _frameFactory.Setup();
            }
        }

        /// <summary>
        /// Write data thats comes from the network
        /// </summary>
        /// <param name="buffer">the received buffer</param>
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
        /// <param name="disposing">the dispose indicator</param>
        protected override void Dispose( bool disposing )
        {
            lock ( SyncRoot )
            {
                if ( disposing )
                {
                    _frameFactory.Dispose();
                }

                base.Dispose( disposing );
            }
        }
    }
}
