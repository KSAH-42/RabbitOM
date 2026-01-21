using System;

namespace RabbitOM.Streaming.Net.Rtp.H264
{
    public class H264FrameBuilder : RtpFrameBuilder , IConfigurer<H264FrameBuilderConfiguration>
    {
        private readonly H264FrameFactory _frameFactory = new H264FrameFactory();
        
        public void Configure( H264FrameBuilderConfiguration configuration )
        {
            _frameFactory.Configure( configuration );
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                _frameFactory.Dispose();
            }

            base.Dispose( disposing );
        }

        protected override void OnCleared( RtpClearedEventArgs e )
        {
            _frameFactory.Clear();

            base.OnCleared( e );
        }

        protected override void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            e.CanContinue = RtpPacket.IsDynamicType( e.Packet );

            base.OnPacketAdding( e );
        }

        protected override void OnSequenceCompleted( RtpSequenceEventArgs e )
        {
            base.OnSequenceCompleted( e );

            if ( _frameFactory.TryCreateFrame( e.Packets , out var frame ) )
            {
                OnMediaBuilded( new RtpMediaBuildedEventArgs( frame ) );
            }
        }
    }
}
