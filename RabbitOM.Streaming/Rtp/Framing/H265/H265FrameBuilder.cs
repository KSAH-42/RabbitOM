﻿using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing.H265
{
    public sealed class H265FrameBuilder : RtpFrameBuilder
    {
        private readonly H265FrameBuilderConfiguration _configuration;

        private readonly H265FrameFactory _frameFactory;

        private readonly H265FrameAggregator _aggregator;
    




        public H265FrameBuilder()
        {
#if !DEBUG
            throw new NotImplementedException( "the implementation is not finished, this class must not be used in production." );
#endif
            _configuration = new H265FrameBuilderConfiguration();
            _frameFactory  = new H265FrameFactory();
            _aggregator    = new H265FrameAggregator( this );
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

            IEnumerable<RtpFrame> frames = null;

            lock ( SyncRoot )
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
                _aggregator.Dispose();
                _frameFactory.Dispose();
            }
        }
    }
}
