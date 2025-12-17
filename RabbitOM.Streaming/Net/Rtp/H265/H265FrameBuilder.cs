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

        private readonly H265FrameFactory _frameFactory;

        private readonly H265FrameAggregator _aggregator;
    




        /// <summary>
        /// Initialize a new instance of a H265 frame builder
        /// </summary>
        public H265FrameBuilder()
        {
            _configuration = new H265FrameBuilderConfiguration();
            _frameFactory  = new H265FrameFactory( _configuration );
            _aggregator    = new H265FrameAggregator( _configuration );
        }





        /// <summary>
        /// Gets the configuration used by the builder
        /// </summary>
        public H265FrameBuilderConfiguration Configuration
        {
            get => _configuration;
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
