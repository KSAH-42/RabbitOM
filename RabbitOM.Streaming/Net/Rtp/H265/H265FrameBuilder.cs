using System;

namespace RabbitOM.Streaming.Net.Rtp.H265
{
    public class H265FrameBuilder : RtpFrameBuilder , IConfigurer<H265FrameBuilderConfiguration>
    {
        private readonly H265FrameFactory _frameFactory = new H265FrameFactory();

        public void Configure( H265FrameBuilderConfiguration settings )
        {
            _frameFactory.Configure( settings );
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                _frameFactory.Dispose();
            }

            base.Dispose( disposing );
        }

        protected override void OnCleared( EventArgs e )
        {
            _frameFactory.Clear();

            base.OnCleared( e );
        }

        protected override void OnPacketAdding( RtpPacketAddingEventArgs e )
        {
            e.CanContinue = RtpPacket.IsDynamicType( e.Packet );
            
            base.OnPacketAdding( e );
        }

        protected override void OnSequenceCompleted( RtpSequenceCompletedEventArgs e )
        {
            base.OnSequenceCompleted( e );

            if ( _frameFactory.TryCreateFrame( e.Packets , out var frame ) )
            {
                OnBuilded( new RtpBuildEventArgs( frame ) );
            }
        }
    }
}
