using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    public sealed class H265FrameBuilder : RtpFrameBuilder
    {
        private readonly H265FrameBuilderConfiguration _configuration;

        private readonly H265FrameFactory _frameFactory;

        private readonly H265FrameAggregator _aggregator;
    




        public H265FrameBuilder()
        {
            _configuration = new H265FrameBuilderConfiguration();
            _frameFactory  = new H265FrameFactory( _configuration );
            _aggregator    = new H265FrameAggregator( _configuration );
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

            Console.WriteLine( "PacketType {0} {1}" , packet.Type , packet.Payload.Count );

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
